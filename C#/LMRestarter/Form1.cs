using System;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LMRestarter
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Password for authentication
        /// </summary>
        private String _password = "";

        /// <summary>
        /// Current server
        /// </summary>
        private String _server = @"lordmancer.ru";
        
        /// <summary>
        /// Constructor
        /// </summary>
        public Form1() {
            InitializeComponent();
            cmbServer.SelectedIndex = 0;
        }

        /// <summary>
        /// Performs the request to Node.js server
        /// </summary>
        /// <param name="url">server's URL</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        private String doRequest(String url, String password)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = @"text/xml;charset=""utf-8""";
            request.Method = "GET";
            request.KeepAlive = false;
            if (!String.IsNullOrWhiteSpace(password))
                request.Headers.Add("password", password);
            var res = "";
            var locker = new ManualResetEvent(false);
            
            var asynch = request.BeginGetResponse(result => {
                try {
                    using (var response = (HttpWebResponse)(request.EndGetResponse(result)))
                    {
                        var stream = response.GetResponseStream();
                        if (stream == null) return;
                        var buffer = new byte[response.ContentLength];
                        stream.Read(buffer, 0, (int)response.ContentLength);
                        res = Encoding.UTF8.GetString(buffer);
                    }
                }
                catch (Exception e) {
                    res = e.ToString();
                    request.Abort();
                }
                finally {
                    locker.Set();
                }
            }, null);

            ThreadPool.RegisterWaitForSingleObject(asynch.AsyncWaitHandle, (obj, timedOut) => {
                if (timedOut) {
                    res = "timed out";
                    request.Abort();
                }
            }, null, 5000, true);

            locker.WaitOne();
            return res;
        }

        private void BtnAuthClick(object sender, EventArgs e)
        {
            if (!GBoxAuth.Visible) {
                var s = doRequest(String.Format("http://{0}:20008/register", _server), "");
                if (s.Contains(@"Exception")) {
                    MessageBox.Show(@"No connection to Node.js server");
                    return;
                }
                GBoxAuth.Visible = true;
                TboxAuth.Focus();
                BtnAuth.Text = @"Come on!";
            }
            else {
                _password = TboxAuth.Text.Trim();
                if (doRequest(String.Format("http://{0}:20008/isrun", _server), _password).StartsWith("Not auth"))
                {
                    MessageBox.Show(@"Wrong password!");
                    return;
                }
                panel2.Dock = DockStyle.Fill;
                new Thread(() => {
                    while (!IsDisposed)
                        try {
                            var s = doRequest(String.Format("http://{0}:20008/isrun", _server), _password);
                            if (s.StartsWith("Not auth")) {
                                MessageBox.Show(@"Your password is expired! Sign in again!");
                                Invoke((MethodInvoker)(() => LblStatus.Text = ""));
                                return;
                            }
                            var isrun = Boolean.Parse(s);
                            Invoke((MethodInvoker)(() => {
                                LblStatus.Text = isrun ? "Server is running" : "Server is stopped";
                                LblStatus.ForeColor = isrun ? Color.Green : Color.Red;
                                BtnStart.Enabled = !isrun;
                            }));
                            Thread.Sleep(6000);
                        }
                        catch (Exception ex) {MessageBox.Show(ex.ToString());}
                }).Start();
            }
        }

        private void BtnStopClick(object sender, EventArgs e)
        {
            var s = doRequest(String.Format("http://{0}:20008/stop", _server), _password);
            MessageBox.Show(s.StartsWith("Not auth") ? @"Your password is expired!" : "Server sent: " + s);
        }

        private void BtnKillClick(object sender, EventArgs e)
        {
            var s = doRequest(String.Format("http://{0}:20008/kill", _server), _password);
            MessageBox.Show(s.StartsWith("Not auth") ? @"Your password is expired" : "Server sent: " + s);
        }

        private void BtnWipeoutClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"This action is NOT recommended! Continue?", "", MessageBoxButtons.YesNoCancel) != DialogResult.Yes) return;
            var s = doRequest(String.Format("http://{0}:20008/wipeout", _server), _password);
            MessageBox.Show(s.StartsWith("Not auth") ? @"Your password is expired" : "Server sent: " + s);
        }

        private void BtnStartClick(object sender, EventArgs e)
        {
            var s = doRequest(String.Format("http://{0}:20008/start", _server), _password);
            MessageBox.Show(s.StartsWith("Not auth") ? @"Your password is expired" : "Server sent: " + s);
        }

        private void TboxAuthKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnAuthClick(sender, null);
        }

        private void CmbServerIndexChanged(object sender, EventArgs e)
        {
            _server = cmbServer.SelectedItem.ToString();
        }
    }
}
