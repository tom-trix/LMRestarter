goog.provide('ru.mobak.lordmancer.Restarter');

goog.require('goog.dom');
goog.require('goog.style');
goog.require('goog.events');
goog.require('goog.dom.forms');
goog.require('goog.net.XhrIo');
goog.require('goog.ui.Dialog');
goog.require('goog.ui.LabelInput');


/**
 * Lite client to restart the Lordmancer Server
 * @constructor
 */
ru.mobak.lordmancer.Restarter = function() {
    /**
     * Parent of all the elements
     * @type {HTMLElement}
     */
    this.mainDiv = goog.dom.getElement('mainDiv');
    /**
     * Password to authenticate a user
     * @type {string}
     */
    this.password = '-1';
    /**
     * Server's url
     * @type {string}
     */
    this.url = 'http://lordmancer.ru:20008';
};


/**
 * Shows the screen to authenticate a user
 */
ru.mobak.lordmancer.Restarter.prototype.showAuthenticateButton = function() {
    // flush the page
    goog.dom.removeChildren(this.mainDiv);
    // create an authenticate button
    var authenticateButton = goog.dom.createDom('div', {'class': 'authenticateButton'}, 'Вход');
    goog.events.listen(authenticateButton, goog.events.EventType.CLICK, goog.bind(function() {
        //goog.net.XhrIo.send(this.url + '/register');
        var dialog = new goog.ui.Dialog(undefined, true);
        dialog.setContent('<input label="Введите пароль..."/>');
        dialog.setButtonSet(goog.ui.Dialog.ButtonSet.createOk());
        dialog.setTitle('Введите пароль (действует 20 мин.)');
        var input = goog.dom.getChildren(dialog.getContentElement())[0];
        (new goog.ui.LabelInput).decorate(input);
        goog.events.listen(dialog, goog.ui.Dialog.EventType.SELECT, goog.bind(function() {
            this.password = input.value.trim();
            goog.bind(this.showMainScreen, this)();
            dialog.setVisible(false);
        }, this));
        dialog.setVisible(true);
        input.focus();
    }, this));
    goog.dom.appendChild(this.mainDiv, authenticateButton);
};


/**
 * Shows the main screen
 */
ru.mobak.lordmancer.Restarter.prototype.showMainScreen = function() {
    // flush the page
    goog.dom.removeChildren(this.mainDiv);
    // create a button to stop the server
    var stopButton = goog.dom.createDom('div', {'class': 'button'}, 'Stop');
    goog.events.listen(stopButton, goog.events.EventType.CLICK, goog.bind(function() {
        goog.net.XhrIo.send(this.url + '/stop', null, 'GET', '', {'password': this.password});
        this.checkIsServerRun();
    }, this));
    goog.dom.appendChild(this.mainDiv, stopButton);
    // create a button to kill the server
    var killButton = goog.dom.createDom('div', {'class': 'button'}, 'Kill');
    goog.events.listen(killButton, goog.events.EventType.CLICK, goog.bind(function() {
        goog.net.XhrIo.send(this.url + '/kill', null, 'GET', '', {'password': this.password});
        this.checkIsServerRun();
    }, this));
    goog.dom.appendChild(this.mainDiv, killButton);
    // create a button to wipe the server out
    var wipeoutButton = goog.dom.createDom('div', {'class': 'button'}, 'Wipe out');
    goog.events.listen(wipeoutButton, goog.events.EventType.CLICK, goog.bind(function() {
        var dialog = new goog.ui.Dialog(undefined, true);
        dialog.setButtonSet(goog.ui.Dialog.ButtonSet.createYesNoCancel());
        dialog.setTitle('Внимание! Данное дейсвтие не рекомендуется. Продолжить?');
        goog.events.listen(dialog, goog.ui.Dialog.EventType.SELECT, goog.bind(function(e) {
            if (e.key != 'yes') return;
            goog.net.XhrIo.send(this.url + '/wipeout', null, 'GET', '', {'password': this.password});
            this.checkIsServerRun();
            dialog.setVisible(false);
        }, this));
        dialog.setVisible(true);
    }, this));
    goog.dom.appendChild(this.mainDiv, wipeoutButton);
    // create a button to start the server
    var startButton = goog.dom.createDom('div', {'id': 'startButton', 'class': 'button'}, 'Start');
    goog.events.listen(startButton, goog.events.EventType.CLICK, goog.bind(function() {
        goog.net.XhrIo.send(this.url + '/start', null, 'GET', '', {'password': this.password});
        this.checkIsServerRun();
    }, this));
    goog.dom.appendChild(this.mainDiv, startButton);
    // create a colour indicator (green = server works, red = server stopped)
    var colourIndicator = goog.dom.createDom('div', {'id': 'indicator', 'class': 'colourIndicatorGreen'}, '_');
    var timer = new goog.Timer(10000);
    goog.events.listen(timer, goog.Timer.TICK, goog.bind(function() {
        this.checkIsServerRun();
    }, this));
    this.checkIsServerRun();
    timer.start();
    goog.dom.appendChild(this.mainDiv, colourIndicator);
};

/**
 * Checks whether the Lordmancer Server is switched on and handles some of the elements on the page if any
 */
ru.mobak.lordmancer.Restarter.prototype.checkIsServerRun = function() {
    goog.net.XhrIo.send(this.url + '/isrun', function(e) {
        var response = e.target.getResponseText();
        window.console.log(e);
        window.console.log(response);
        goog.dom.classes.swap(goog.dom.getElement('indicator'), response ? 'colourIndicatorRed' : 'colourIndicatorGreen', response ? 'colourIndicatorGreen' : 'colourIndicatorRed');
        goog.dom.forms.setDisabled(goog.dom.getElement('startButton'), response);
    }, 'GET', '', {'password': this.password});
};
