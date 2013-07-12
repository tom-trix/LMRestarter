var express = require('express');
var process = require('child_process').exec;
var XMLHttpRequest = require("xmlhttprequest").XMLHttpRequest;
var server = express();
var password = -1;

/** Авторизация и выдача пароля на 20 мин. */
server.get('/register', function(request, response) {
    password = Math.floor(Math.random() * 900000) + 100000;
    setTimeout(function() {
        password = -1;
        console.log('Password flushed');
    }, 20 * 60 * 1000);
    console.log('Password generated: ' + password);
    sendSMS(79028057441);
    sendSMS(79638833995);
    response.send('true');
});

/** Проверка, запущен ли процесс */
server.get('/isrun', function(request, response) {
    handle('if [ -d "/proc/$(cat ../mancer.pid)" ]; then echo true; else echo false; fi', request, response);
});

/** Остановка сервера */
server.get('/stop', function(request, response) {
    handle('./mancer-stop; echo true', request, response);
});

/** Убийство сервера */
server.get('/kill', function(request, response) {
    handle('./mancer-kill; echo true', request, response);
});

/** Зверское убийство сервера */
server.get('/wipeout', function(request, response) {
    handle('kill -5 $(cat ../mancer.pid); echo true', request, response);
});

/** Запуск сервера (+ проверка, не запущен ли процесс, заданный в PID) */
server.get('/start', function(request, response) {
    handle('if [ -d "/proc/$(cat ../mancer.pid)" ]; then echo "Unable to start!"; else ./mancer-start; echo true; fi', request, response);
});


/**
 * Обработка команды в bash и отправка результата
 * @param bash скрипт
 * @param request объект Request
 * @param response объект Response
 */
function handle(bash, request, response) {
    console.log(new Date + ': Query ' + request.url);
    if (password > 0 && request.headers['password'] == password)
        process(bash, function (error, stdout, stderr) {
            var result = error !== null ? error + stderr : stdout;
            console.log(result);
            response.send(result);
        });
    else response.send('Not authenticated ');
}


/**
 * Посылает SMS с паролем
 * @param number номер (в формате 79028057441)
 */
function sendSMS(number) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open('GET', 'http://office.mobak.ru/app/mancer-send.php?userNumber=' + number + '&message=(Rus)%20Password%20' + password, true);
    xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200)
            if (xmlhttp.responseText == 0)
                console.log(new Date + ': SMS sent successfully');
            else console.log(new Date + ': Failed to send SMS')
    };
    xmlhttp.send(null);
}


/** Go */
server.listen(20008, function() {
    console.log(new Date + ": LMRestarter run on port 20008");
});
