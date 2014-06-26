var initializeTetrominoModule = function () {
    'use strict';

    /*
    This is the tetris shape object factory closure. It contains the hardcoded shape templates with
    every possible rotation. Only the getRandomShapeAtPosition() function is visible to the outside world.
    */

    var S_SHAPE_TEMPLATE =
        [
            [
                '.....',
                '..OO.',
                '.OO..',
                '.....',
                '.....'
            ],
            [
                '.....',
                '..O..',
                '..OO.',
                '...O.',
                '.....'
            ]
        ],
        Z_SHAPE_TEMPLATE =
        [
            [
                '.....',
                '.OO..',
                '..OO.',
                '.....',
                '.....'
            ],
            [
                '.....',
                '..O..',
                '.OO..',
                '.O...',
                '.....'
            ]
        ],
        I_SHAPE_TEMPLATE =
        [
            [
                '..O..',
                '..O..',
                '..O..',
                '..O..',
                '.....'
            ],
            [
                '.....',
                '.....',
                'OOOO.',
                '.....',
                '.....'
            ]
        ],
        O_SHAPE_TEMPLATE =
        [
            [
                '.....',
                '.....',
                '.OO..',
                '.OO..',
                '.....'
            ]
        ],
        J_SHAPE_TEMPLATE =
        [
            [
                '.....',
                '.O...',
                '.OOO.',
                '.....',
                '.....'
            ],
            [
                '.....',
                '..OO.',
                '..O..',
                '..O..',
                '.....'
            ],
            [
                '.....',
                '.....',
                '.OOO.',
                '...O.',
                '.....'
            ],
            [
                '.....',
                '..O..',
                '..O..',
                '.OO..',
                '.....'
            ]
        ],
        L_SHAPE_TEMPLATE =
        [
            [
                '.....',
                '...O.',
                '.OOO.',
                '.....',
                '.....'
            ],
            [
                '.....',
                '..O..',
                '..O..',
                '..OO.',
                '.....'
            ],
            [
                '.....',
                '.....',
                '.OOO.',
                '.O...',
                '.....'
            ],
            [
                '.....',
                '.OO..',
                '..O..',
                '..O..',
                '.....'
            ]
        ],
        T_SHAPE_TEMPLATE =
        [
            [
                '.....',
                '..O..',
                '.OOO.',
                '.....',
                '.....'
            ],
            [
                '.....',
                '..O..',
                '..OO.',
                '..O..',
                '.....'
            ],
            [
                '.....',
                '.....',
                '.OOO.',
                '..O..',
                '.....'
            ],
            [
                '.....',
                '..O..',
                '.OO..',
                '..O..',
                '.....'
            ]
        ];

    var PIECES = [
        S_SHAPE_TEMPLATE,
        Z_SHAPE_TEMPLATE,
        I_SHAPE_TEMPLATE,
        O_SHAPE_TEMPLATE,
        J_SHAPE_TEMPLATE,
        L_SHAPE_TEMPLATE,
        T_SHAPE_TEMPLATE
    ];

    function getRandomNumber(min, max) {
        return (Math.random() * (max - min) + min) | 0;
    }

    function getRandomColor(min,max){
        return 'rgb(' +
            getRandomNumber(min,max) + ',' +
            getRandomNumber(min,max) + ',' +
            getRandomNumber(min,max) + ')';
    }

    var Shape = function (shape, state, color, x, y) {
        this.shape = shape;
        this.state = state;
        this.color = color;
        this.x = x;
        this.y = y;
    };

    Shape.prototype.getCurrentState = function () {
        return this.shape[this.state];
    };

    Shape.prototype.rotate = function (clockwise) {
        if (!clockwise) {
            this.state -= 1;
            if (this.state < 0) {
                this.state = this.shape.length - 1;
            }
        } else {
            this.state = (this.state + 1) % this.shape.length;
        }
    };

    var getRandomShapeAtPosition = function (x, y) {
        var shape = PIECES[getRandomNumber(0, PIECES.length)];
        var state = getRandomNumber(0, shape.length);
        var color = getRandomColor(50,255);

        return new Shape(shape, state, color, x, y);
    };

    return {
        getRandomShapeAtPosition: getRandomShapeAtPosition
    }
};