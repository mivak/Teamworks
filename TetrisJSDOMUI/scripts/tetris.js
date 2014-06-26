window.onload = function () {
    'use strict';

    //constants
    var BLOCK_SIZE = 20,
        BLOCK_SYMBOL = 'O',
        BOARD_WIDTH = 15,
        BOARD_HEIGHT = 25,
        METRICS_BAR_WIDTH = 200,
        MAX_WIDTH = BLOCK_SIZE * BOARD_WIDTH + METRICS_BAR_WIDTH,
        MAX_HEIGHT = BLOCK_SIZE * BOARD_HEIGHT,
        METRICS_LEFT_OFFSET = BLOCK_SIZE * BOARD_WIDTH + BLOCK_SIZE * 2,
        METRICS_TOP_OFFSET = 75,
        SHAPE_INITIAL_X = BLOCK_SIZE * Math.floor(BOARD_WIDTH / 2 - 2),
        SHAPE_INITIAL_Y = -BLOCK_SIZE * 5,
        INITIAL_GAME_SPEED = 500,
        FALLING_SPEED = BLOCK_SIZE,
        BOARD_BORDER_COLOR = '#fff',
        BOARD_BG_COLOR = '#000',
        POINTS_TO_INCREASE_LEVEL = 100;

    //key codes
    var UP_ARROW = 38, DOWN_ARROW = 40, LEFT_ARROW = 37, RIGHT_ARROW = 39;

    //game variables
    var board,
        score,
        level,
        tetromino,
        fallingShape,
        nextShape,
        gameSpeedInMs,
        animation,
        isGameStart,
        isGameOver,
        tetrisSound,
        isMusicOn,
        isPause,
        interval,
        paper,
        logo;

    var canvas,
        ctx;

    /*-----------------------GAME INITIALIZATION----------------------------------------------------------------*/
    function initializeGame() {
        canvas = $('#animation-container')[0];
        canvas.width = MAX_WIDTH;
        canvas.height = MAX_HEIGHT;
        ctx = canvas.getContext('2d');
        isMusicOn = true;
        isPause = false;
        tetrisSound = document.getElementById('tetrisSound');
        isGameStart = false;
        paper = Raphael(BOARD_WIDTH * 21.5, 400, 300, 70);
        resetGameVariables();
    }

    function resetGameVariables() {
        board = getEmptyGameBoard();
        score = 0;
        level = 1;
        tetromino = initializeTetrominoModule();
        fallingShape = tetromino.getRandomShapeAtPosition(SHAPE_INITIAL_X, SHAPE_INITIAL_Y);
        nextShape = tetromino.getRandomShapeAtPosition(SHAPE_INITIAL_X, SHAPE_INITIAL_Y);
        gameSpeedInMs = INITIAL_GAME_SPEED;
        animation = null;
        isGameOver = false;
        drawLogo(paper);
    }

    function getEmptyGameBoard() {
        var emptyBoard = [];
        for (var i = 0; i < BOARD_HEIGHT; i++) {
            emptyBoard.push(new Array(BOARD_WIDTH));
        }
        return emptyBoard;
    }

    /*-----------------------RENDERING FUNCTIONS----------------------------------------------------------------*/

    function drawLogo(paper) {
        paper.rect(20, 0, 10, 10).attr({
            fill: '#CA4F5C',
            stroke: '#CA4F5C'
        });

        paper.rect(5, 0, 10, 10).attr({
            fill: '#1E6AE6',
            stroke: '#1E6AE6'
        });

        paper.rect(5, 15, 10, 10).attr({
            fill: '#289E21',
            stroke: '#289E21'
        });

        paper.rect(5, 30, 10, 10).attr({
            fill: '#F8C94E',
            stroke: '#F8C94E'
        });

        paper.text(20, 28, 'Tetris').attr({
            'text-anchor': 'start',
            'font-size': 40,
            fill: '#3D95FF'
        });

        paper.path('M 140 5 L 180 5 L 178 35 L 160 37 L 142 35 Z').attr({
            stroke: '#F05028',
            'stroke-width': 2,
            fill: '#F05028'
        });

        paper.path('M 160 5 L 180 5 L 178 35 L160 37 Z').attr({
            stroke: '#F05028',
            'stroke-width': 2,
            fill: '#F1662B'
        });

        paper.path('M 150 10 L 170 10 L L 169 15 L 156 15 L 157 20 H 168 L 166 31 L 160 33 L 154 31 ' +
            'L 153 26 H 158 V 28 L 160 29 L 162 28 L 163 24 H 153 Z').attr({
            stroke: 'none',
            fill: 'white'
        })
    }

    function drawBoard() {
        ctx.strokeStyle = BOARD_BORDER_COLOR;
        ctx.fillStyle = BOARD_BG_COLOR;
        ctx.fillRect(0, 0, BOARD_WIDTH * BLOCK_SIZE, BOARD_HEIGHT * BLOCK_SIZE);
        ctx.strokeRect(0, 0, BOARD_WIDTH * BLOCK_SIZE, BOARD_HEIGHT * BLOCK_SIZE);

        // draw board blocks
        for (var i = 0; i < BOARD_HEIGHT; i++) {
            for (var j = 0; j < BOARD_WIDTH; j++) {
                if (board[i][j]) {
                    ctx.strokeStyle = '#fff';
                    ctx.fillStyle = board[i][j];
                    ctx.strokeRect(j * BLOCK_SIZE, i * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);
                    ctx.fillRect(j * BLOCK_SIZE, i * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);
                }
            }
        }
    }

    function drawShape(shape, offsetX, offsetY) {
        var currState = shape.getCurrentState();
        for (var r = 0; r < currState.length; r++) {
            for (var c = 0; c < currState[r].length; c++) {
                if (currState[r][c] === BLOCK_SYMBOL) {
                    ctx.fillStyle = shape.color;
                    ctx.strokeStyle = '#fff';
                    ctx.fillRect(
                            offsetX + BLOCK_SIZE * c,
                            offsetY + BLOCK_SIZE * r,
                        BLOCK_SIZE, BLOCK_SIZE
                    );
                    ctx.strokeRect(
                            offsetX + BLOCK_SIZE * c,
                            offsetY + BLOCK_SIZE * r,
                        BLOCK_SIZE, BLOCK_SIZE
                    );
                }
            }
        }
    }

    function drawMetrics() {
        ctx.strokeStyle = BOARD_BORDER_COLOR;
        ctx.strokeRect(
            METRICS_LEFT_OFFSET,
            METRICS_TOP_OFFSET,
            80, 80);
        drawShape(nextShape, METRICS_LEFT_OFFSET, METRICS_TOP_OFFSET);
        ctx.fillStyle = BOARD_BORDER_COLOR;
        ctx.font = '30px Consolas';
        ctx.fillText("Score " + score,
            METRICS_LEFT_OFFSET - BLOCK_SIZE,
            METRICS_TOP_OFFSET * 3);
        ctx.fillText("Level " + level,
                METRICS_LEFT_OFFSET - BLOCK_SIZE,
                METRICS_TOP_OFFSET * 4);
    }

    function renderAll() {
        ctx.clearRect(0, 0, MAX_WIDTH, MAX_HEIGHT);
        drawBoard();
        drawShape(fallingShape, fallingShape.x, fallingShape.y);
        drawMetrics();
    }

    /*----------------------------------------Game logic----------------------------------------------*/

    function addToBoard(currState) {
        for (var r = 0; r < currState.length; r++) {
            for (var c = 0; c < currState[r].length; c++) {
                if (currState[r][c] === BLOCK_SYMBOL) {
                    var tempX = fallingShape.x + BLOCK_SIZE * c;
                    var tempY = fallingShape.y + BLOCK_SIZE * r;
                    var boardRow = tempY / BLOCK_SIZE;
                    var boardCol = tempX / BLOCK_SIZE;
                    if (boardRow >= 0) {
                        board[boardRow][boardCol] = fallingShape.color;
                    }
                }
            }
        }
    }

    function updateScore() {
        score += level * BOARD_WIDTH;

        if (score > level * level * POINTS_TO_INCREASE_LEVEL) {
            level += 1;
        }
    }

    function checkForCompleteLines() {
        var isCompleteLine;
        for (var r = board.length - 1; r >= 0; r--) {
            isCompleteLine = true;
            for (var c = 0; c < board[r].length; c++) {
                if (!board[r][c]) {
                    isCompleteLine = false;
                    break;
                }
            }
            if (isCompleteLine) {
                updateScore();
                for (var row = r; row > 0;) {
                    board[row] = board[--row];
                }
                board[0] = new Array(BOARD_WIDTH);
                r++;
            }
        }
    }

    function checkForGameOver() {
        for (var i = 0; i < board[1].length; i++) {
            var box = board[0][i];

            if (box) {
                isGameOver = true;
                return true;
            }
        }
        return false;
    }

    function update() {
        var currState = fallingShape.getCurrentState();
        var onBotom = false;
        for (var r = 0; r < currState.length; r++) {
            for (var c = 0; c < currState[r].length; c++) {
                if (currState[r][c] === BLOCK_SYMBOL) {
                    var tempY = fallingShape.y + BLOCK_SIZE * r;
                    if (tempY >= 0) {
                        if (tempY + BLOCK_SIZE >= BOARD_HEIGHT * BLOCK_SIZE ||
                            board[(tempY + BLOCK_SIZE) / BLOCK_SIZE][(fallingShape.x + BLOCK_SIZE * c) / BLOCK_SIZE]) {
                            onBotom = true;
                            break;
                        }
                    }
                }
            }
        }

        if (onBotom) {
            addToBoard(currState);
            checkForCompleteLines();

            if (checkForGameOver()) {
                gameOver();
                return;
            }

            fallingShape = nextShape;
            ctx.clearRect(nextShape.x + METRICS_LEFT_OFFSET - 1,
                    nextShape.y + METRICS_TOP_OFFSET - 1, 100, 100);
            nextShape = tetromino.getRandomShapeAtPosition(SHAPE_INITIAL_X, SHAPE_INITIAL_Y);

        }
        else {
            fallingShape.y += FALLING_SPEED;
        }
        renderAll();
    }

    function hasLeftLimit() {
        var currState = fallingShape.getCurrentState();
        for (var r = 0; r < currState.length; r++) {
            for (var c = 0; c < currState[r].length; c++) {
                if (currState[r][c] === BLOCK_SYMBOL) {
                    var tempX = fallingShape.x + BLOCK_SIZE * c;
                    var tempY = fallingShape.y + BLOCK_SIZE * r;
                    if (tempY >= 0) {
                        if (tempX <= 0 || board[tempY / BLOCK_SIZE][tempX / BLOCK_SIZE - 1]) {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    function hasRightLimit() {
        var currState = fallingShape.getCurrentState();
        for (var r = 0; r < currState.length; r++) {
            for (var c = 0; c < currState[r].length; c++) {
                if (currState[r][c] === BLOCK_SYMBOL) {
                    var tempX = fallingShape.x + BLOCK_SIZE * c;
                    var tempY = fallingShape.y + BLOCK_SIZE * r;
                    if (tempY >= 0) {
                        if (tempX + BLOCK_SIZE >= BOARD_WIDTH * BLOCK_SIZE
                            || board[tempY / BLOCK_SIZE][(tempX + BLOCK_SIZE) / BLOCK_SIZE] != undefined) {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    function isOverFallenItems() {
        var currState = fallingShape.getCurrentState();
        for (var r = 0; r < currState.length; r++) {
            for (var c = 0; c < currState[r].length; c++) {
                if (currState[r][c] === BLOCK_SYMBOL) {
                    var tempX = fallingShape.x + BLOCK_SIZE * c;
                    var tempY = fallingShape.y + BLOCK_SIZE * r;
                    if (tempY >= 0) {
                        if (board[tempY / BLOCK_SIZE][tempX / BLOCK_SIZE] != undefined) {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    function findMinMaxX() {
        var currState = fallingShape.getCurrentState();
        var minX = 0;
        var maxX = BOARD_WIDTH*BLOCK_SIZE-BLOCK_SIZE;
        for (var r = 0; r < currState.length; r++) {
            for (var c = 0; c < currState[r].length; c++) {
                if (currState[r][c] === BLOCK_SYMBOL) {
                    var tempX = fallingShape.x + BLOCK_SIZE * c;
                    if (tempX < minX) {
                        minX = tempX
                    }

                    if (tempX > maxX) {
                        maxX = tempX;
                    }
                }
            }
        }

        return { left: minX, right: maxX };
    }

    function rotateIfPossible() {
        fallingShape.rotate(true);
        var isOverAnotherItem = isOverFallenItems(); // check the new state for colisions

        if (isOverAnotherItem) {
            fallingShape.rotate(false); // restore initial state
        }
    }

    function playSound(sound, stopIt) {
        if (stopIt) {       //this is to restart the sound when another button is pressed
            if (isMusicOn) {
                if (sound.paused) {
                    sound.play();
                } else {
                    sound.currentTime = 0
                }
            }
        }
        else {
            if (sound.paused) {     //when bakcground music stop, restart it
                sound.play();
            }
        }
    }

    $('body').on('keydown', function (ev) {

        if (!isGameOver && !isPause) {
            var sound = document.getElementById('soundButtonClicked');

            switch (ev.keyCode) {
                case UP_ARROW:
                    playSound(sound, true);
                    var limitedOnLeft = hasLeftLimit(fallingShape);
                    var limitedOnRight = hasRightLimit(fallingShape);

                    if (limitedOnLeft && limitedOnRight) { // some of the figures could be rotated with limits on both sides
                        rotateIfPossible();
                        break;
                    }
                    else {
                        if (limitedOnLeft && !limitedOnRight) {
                            rotateIfPossible();
                            var boundaries = findMinMaxX();

                            if (boundaries.left < 0) { // this could be true if the element has been rotated
                                fallingShape.x += -boundaries.left;
                                var isOverAnotherItem = isOverFallenItems(); // check the new position for colisions

                                if (isOverAnotherItem) { // rotation is not possible
                                    fallingShape.x += boundaries.left; // restore position
                                    fallingShape.rotate(false); // restore initial state
                                }
                            }

                        }
                        else if (limitedOnRight && !limitedOnLeft) {
                            rotateIfPossible();
                            var boundaries = findMinMaxX();

                            if (boundaries.right >= BOARD_WIDTH * BLOCK_SIZE) { // this could be true if the element has been rotated
                                fallingShape.x -= (boundaries.right - BOARD_WIDTH * BLOCK_SIZE + BLOCK_SIZE);
                                var isOverAnotherItem = isOverFallenItems(); // check the new position for colisions

                                if (isOverAnotherItem) { // rotation is not possible
                                    fallingShape.x += (boundaries.right - BOARD_WIDTH * BLOCK_SIZE + BLOCK_SIZE); // restore position
                                    fallingShape.rotate(false); // restore initial state
                                }
                            }
                        }
                        else { // !limitedOnRight && !limitedOnLeft
                            rotateIfPossible();
                        }

                        renderAll();
                        break;
                    }

                case DOWN_ARROW:
                    playSound(sound, true);
                    update();
                    break;
                case LEFT_ARROW:
                    playSound(sound, true);
                    var hasLeftLimitation = hasLeftLimit(fallingShape);
                    if (!hasLeftLimitation) {
                        fallingShape.x -= FALLING_SPEED;
                    }
                    renderAll();
                    break;
                case RIGHT_ARROW:
                    playSound(sound, true);
                    var hasRightLimitation = hasRightLimit(fallingShape);
                    if (!hasRightLimitation) {
                        fallingShape.x += FALLING_SPEED;
                    }
                    renderAll();
                    break;
            }
        }
    });

    $('#startBtn').on('click', function () {
        isGameStart = true;
        clearInterval(animation);
        resetGameVariables();

        interval = gameSpeedInMs - ((level - 1) * 50);

        animation = setInterval(run, interval);
    });

    function run() {
        update();
        if (isGameOver) {
            return;
        }
        renderAll();

        if (isMusicOn) {
            playSound(tetrisSound, false);
        }

        if (interval !== gameSpeedInMs - ((level - 1) * 50)) {
            clearInterval(animation);
            interval = gameSpeedInMs - ((level - 1) * 50);

            if (interval < 50) {
                interval = 50;
            }

            animation = setInterval(run, interval);
        }
    }

    function makeButton(parent, button, id) {
        var newButton = document.createElement(button);

        if (id !== '') {
            newButton.setAttribute("id", id);
        }

        if (parent == '') {
            parent = 'body';
        }

        $(parent).append(newButton);
        return newButton;
    }

    $('#musicBtn').click(function () {
        if (isMusicOn) {
            isMusicOn = false;
        }
        else {
            isMusicOn = true;
        }
        switchMusic();
    });

    function switchMusic() {
        if (isMusicOn) {
            if (isGameStart) {      //Music starts when the game starts
                tetrisSound.play();
            }
            $("#musicBtn").text("Music OFF");
        }
        else {
            tetrisSound.pause();
            $("#musicBtn").text("Music ON");
        }
    };

    $('#pauseBtn').on('click', function () {
        if (isGameStart) {
            if (!isPause) {
                isPause = true;
                window.clearInterval(animation);
            }
            else {
                isPause = false;
                animation = setInterval(run, interval);
            }
        }
    });

    //RUN
    $('document').ready(function () {
        initializeGame();
        drawBoard();
    });

    function gameOver() {
        isGameStart = false;
        clearInterval(animation);
        endGameState(score);
    }

    function endGameState(score) {
        ctx.clearRect(0, 0, MAX_WIDTH, MAX_HEIGHT);
        $("#startBtn").css("display", "none");
        $("#pauseBtn").css("display", "none");
        $("#musicBtn").css("display", "none");
        paper.clear();
        logo = Raphael(BOARD_WIDTH * 10.5, 400, 300, 70);
        drawLogo(logo);

        ctx.font = "40px Arial";
        ctx.fillText("GAME OVER!", 120, 50);

        ctx.font = "20px Arial";
        ctx.fillText("Please enter your name", 140, 120);
        ctx.fillText("for the high score table", 145, 160);

        var input = $('<input/>')
                .css("position", "absolute")
                .css("left", "190px")
                .css("top", "230px")
                .attr("id", "input_name");

        var buttonSave = $('<button/>')
                .css("position", "absolute")
                .css("left", "235px")
                .css("top", "285px")
                .html("Save")
                .addClass("beautifull")
                .attr("id", "btn_save")
                .click(function () {
                    ctx.font = "40px Arial";
                    ctx.fillText("Loading...", 120, 50);

                    $.post("http://tetris-8.apphb.com/api/Players",
                          { "Name": $("#input_name").val(), "Score": score },
                          function (responce) {
                              highScores();
                          });
                });

        var buttonHighScores = $('<button/>')
                .css("position", "absolute")
                .css("left", "211px")
                .css("top", "322px")
                .html("High Scores")
                .addClass("beautifull")
                .attr("id", "btn_highscores")
                .click(highScores);

        var buttonRetry = $('<button/>')
                .css("position", "absolute")
                .css("left", "235px")
                .css("top", "360px")
                .html("Retry")
                .addClass("beautifull")
                .attr("id", "btn_retry")
                .click(function () {
                    logo.clear();
                    clearBoard();
                });

        $("nav").append(input).append(buttonSave).append(buttonHighScores).append(buttonRetry);
    }

    function clearBoard() {
        $("#btn_prev").remove();
        $("#btn_next").remove();
        $("table").css("display", "none");
        $("#btn_main").remove();
        $("#input_name").remove();
        $("#btn_save").remove();
        $("#btn_highscores").remove();
        $("#btn_retry").remove();
        ctx.clearRect(0, 0, MAX_WIDTH, MAX_HEIGHT);
        $("#startBtn").css("display", "inline-block");
        $("#pauseBtn").css("display", "inline-block");
        $("#musicBtn").css("display", "inline-block");

        drawBoard();
    };

    function highScores() {
        var users, index = 0;

        ctx.clearRect(0, 0, MAX_WIDTH, MAX_HEIGHT);
        $("#input_name").remove();
        $("#btn_save").remove();
        $("#btn_highscores").remove();
        $("#btn_retry").remove();

        ctx.font = "40px Arial";
        ctx.fillText("Loading...", 120, 50);

        var buttonMain = $('<button/>')
                .css("position", "absolute")
                .css("left", "210px")
                .css("top", "465px")
                .html("Main page")
                .addClass("beautifull")
                .attr("id", "btn_main")
                .click(function () {
                    $("#btn_main").remove();
                    $("#btn_next").remove();
                    $("#btn_prev").remove();
                    logo.clear();
                    clearBoard();
                });

        var buttonNext = $('<button/>')
                .css("position", "absolute")
                .css("left", "370px")
                .css("top", "465px")
                .html("Next")
                .addClass("beautifull")
                .attr("id", "btn_next")
                .click(function () {
                    index++;
                    if ((index + 1) * 16 >= users.length) {
                        buttonNext.css("display", "none");
                    }
                    if ($("#btn_prev").css("display") == "none" && index > 0) {
                        $("#btn_prev").css("display", "block");
                    }

                    drawTable(index);
                });

        var buttonPrevious = $('<button/>')
                .css("position", "absolute")
                .css("left", "70px")
                .css("top", "465px")
                .css("display", "none")
                .html("Previous")
                .addClass("beautifull")
                .attr("id", "btn_prev")
                .click(function () {
                    index--;
                    if (index == 0) {
                        buttonPrevious.css("display", "none");
                    }
                    if ($("#btn_next").css("display") == "none" && users.length > 16) {
                        $("#btn_next").css("display", "block");
                    }

                    drawTable(index);
                });


        $.get("http://tetris-8.apphb.com/api/Players",
               function (data) {
                   ctx.clearRect(0, 0, MAX_WIDTH, MAX_HEIGHT);

                   users = data;

                   drawTable(index);

                   if (users.length > 16) {
                       $("nav").append(buttonNext);
                   }
               });

        function drawTable(newIndex) {
            if ($("table").length != 0) {
                $("table").remove();
            }

            var table = $('<table/>')
                    .css("position", "absolute")
                    .css("left", "40px")
                    .css("top", "40px");
            var tr = $('<tr/>');
            var tdName = $('<td/>')
                        .html("Name")
                        .css("width", "340px");
            var tdScore = $('<td/>')
                        .html("Score");

            tr.append(tdName);
            tr.append(tdScore);
            table.append(tr);

            for (var i = index * 16; i < Math.min(index * 16 + 16, users.length) ; i++) {
                var trForPlayer = $('<tr/>');

                var tdForName = $('<td/>')
                                .html(users[i].Name);

                var tdForScore = $('<td/>')
                                .html(users[i].Score);

                trForPlayer.append(tdForName);
                trForPlayer.append(tdForScore);
                table.append(trForPlayer);
            }

            $("#wrapper").append(table);
        }

        $("nav").append(buttonMain).append(buttonPrevious);
    }
};