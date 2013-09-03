package ru.tomtrix.android.lmrestarter

import java.io.*
import java.net.*
import android.os.*
import android.widget.*
import android.util.Log
import android.view.View
import android.app.Activity
import android.os.AsyncTask.Status

public open class TrixActivity() : Activity() {

    var _password = ""
    var handler: Handler? = null

    public override fun onCreate(savedInstanceState: Bundle?): Unit {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.main)
        handler = Handler()
    }

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

    private fun showMsg(s: String?) {
        Toast.makeText(this, s, Toast.LENGTH_SHORT).show()
    }

    public fun btnAuthOnClick(v: View) {
        val txtPswd = findViewById(R.id.txtPswd) as EditText
        val activity = this

        if (txtPswd.getVisibility() == View.INVISIBLE) {
            ///val s = doRequest("http://lordmancer.ru:20008/register", "")
            val s = doRequest("http://lordmancer.ru:20008/isrun", "")
            if (s.contains("Exception")) {showMsg("No connection to Node.js server"); return}
            txtPswd.setVisibility(View.VISIBLE)
            (v as Button).setText("Come on!")
        } else {
            ///_password = txtPswd.getText().toString()
            _password = "214976"
            if (doRequest("http://lordmancer.ru:20008/isrun", _password).startsWith("Not auth")) {showMsg("Wrong password!"); return}
            setContentView(R.layout.second)
            Thread(object : Runnable {
                public override fun run() {
                    while (!activity.isFinishing()) {
                        var s = doRequest("http://lordmancer.ru:20008/isrun", _password);
                        if (s.startsWith("Not auth")) {showMsg("Your password is expired! Sign in again!"); return}
                        val isrun = java.lang.Boolean.parseBoolean(s)
                        Log.d("Trix", "isrun = " + isrun)
                        handler?.post(object : Runnable {
                            public override fun run() {
                                (activity.findViewById(R.id.btnHey) as Button).setText(if (isrun) "Server is running" else "Server is stopped")
                            }
                        })
                        Thread.sleep(6000)
                    }
                }
            }).start()
        }
    }
}
