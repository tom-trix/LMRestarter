package ru.tomtrix.android.lmrestarter

import java.io.*
import java.net.*
import android.os.*
import android.app.*
import android.widget.*
import android.util.Log
import android.view.View
import android.graphics.Color
import android.os.AsyncTask.Status
import android.content.DialogInterface

public open class TrixActivity() : Activity() {

    /** Password for authentication */
    private var _password = ""
    /** Current server */
    private var _server = "lordmancer.ru"
    /** Handler to process GUI-operations inside a non-GUI thread */
    private var _handler: Handler? = null



    /** Entry point */
    public override fun onCreate(savedInstanceState: Bundle?): Unit {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.main)
        _handler = Handler()
    }

    /**
     * Performs the request to Node.js server
     * @param url - server's URL
     * @param password - password
     */
    private fun doRequest(url: String, password: String): String {
        return object : AsyncTask<String, Void, String>() {
            protected override fun doInBackground(vararg p0: String?): String? {
                val connection = URL(p0[0]).openConnection() as URLConnection
                connection.setRequestProperty("password", p0[1])
                val reader = BufferedReader(InputStreamReader(connection.getInputStream() as InputStream))
                val result = reader.readLine()
                reader.close()
                Log.d("Trix", result)
                return result
            }
        }.execute(url, password).get() ?: "Error"
    }

    /** Shows a simple message */
    private fun showMsg(s: String?) {
        Toast.makeText(this, s, Toast.LENGTH_SHORT).show()
    }

    /** Shows a 2-buttons dialog and runs the <b>onRight</b> function if the user clicks a right button */
    private fun showDialog(s: String, rightText: String, leftText: String, onLeftClick: () -> Unit) {
        var builder = AlertDialog.Builder(this)
        builder setMessage s
        if (rightText.size > 0) builder.setNegativeButton(rightText, null)
        builder.setPositiveButton(leftText, object : DialogInterface.OnClickListener {
            public override fun onClick(p0: DialogInterface, p1: Int) {
                onLeftClick()
            }
        })
        builder.create().show()
    }



    public fun btnAuthClick(v: View) {
        val txtPswd = findViewById(R.id.txtPswd) as EditText
        val activity = this

        if (txtPswd.getVisibility() == View.INVISIBLE) {
            val s = doRequest("http://$_server:20008/register", "")
            if (s contains "Exception") {showMsg("No connection to Node.js server"); return}
            txtPswd setVisibility View.VISIBLE
            (v as Button) setText "Come on!"
        } else {
            _password = txtPswd.getText().toString()
            if (doRequest("http://$_server:20008/isrun", _password) startsWith "Not auth") {showMsg("Wrong password!"); return}
            setContentView(R.layout.second)
            Thread(object : Runnable {
                public override fun run() {
                    while (!activity.isFinishing()) {
                        val s = doRequest("http://$_server:20008/isrun", _password);
                        _handler?.post(object : Runnable {
                            public override fun run() {
                                if (s startsWith "Not auth") showDialog("Your password is expired! Sign in again!", "", "OK", {() -> activity.finish()})
                                val isrun = java.lang.Boolean.parseBoolean(s)
                                val lblStatus = activity.findViewById(R.id.lblStatus) as TextView
                                lblStatus setText (if (isrun) "Server is running" else "Server is stopped")
                                lblStatus setTextColor (if (isrun) Color.GREEN else Color.rgb(240, 50, 50))
                                (activity.findViewById(R.id.btnStart) as Button) setEnabled !isrun
                            }
                        })
                        Thread.sleep(6000)
                    }
                }
            }).start()
        }
    }

    public fun btnStopClick(v: View) {
        val s = doRequest("http://$_server:20008/stop", _password)
        showMsg(if (s startsWith "Not auth") "Your password is expired" else "Server sent: $s")
    }

    public fun btnKillClick(v: View) {
        val s = doRequest("http://$_server:20008/kill", _password)
        showMsg(if (s startsWith "Not auth") "Your password is expired" else "Server sent: $s")
    }

    public fun btnWipeoutClick(v: View) {
        showDialog("This action is NOT recommended! Continue?", "No", "Yes", {() ->
            val s = doRequest("http://$_server:20008/wipeout", _password)
            showMsg(if (s startsWith "Not auth") "Your password is expired" else "Server sent: $s")
        })
    }

    public fun btnStartClick(v: View) {
        val s = doRequest("http://$_server:20008/start", _password)
        showMsg(if (s startsWith "Not auth") "Your password is expired" else "Server sent: $s")
    }

    public fun radioServerIndexChanged(v: View) {
        _server = (v as RadioButton).getText().toString()
    }
}
