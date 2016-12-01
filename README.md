![Ingame Default](/../screenshots/ingame-default.png?raw=true "Default setup.")

What is PongOut?
==========

PongOut is a hybrid of the two classic games Pong and Breakout, with the added fun of *powerups*!

Instructions:
==========

• Players each control a paddle which moves up and down and a set of bricks. Use
your paddle to deflect the ball towards your opponent. You score a point each
time the ball goes off your opponent's table edge.

• The bricks are your first line of defense from letting the ball off your table
edge. A brick is destroyed each time it is struck by the ball.

• Powerups are constantly affecting the ball, including:

1. **Rainbow** - ball color is randomized
2. **Jink** - ball dashes up or down
3. **Accelerate** - ball boosts forward

• Hint: you can direct the ball's rebound angle based on it's distance from the
center of your paddle. The further away from the center, the wider the rebound
angle.

*First to 10 points wins!*

Controls:
==========
##### Player 1:

    Move Up - 'W'
    Move Down - 'S'

##### Player 2:

    Move Up - 'I'
    Move Down - 'I'
    
##### Pause

    'escape'
    'enter/return'
    'space bar'
    'p'

Customization:
==========
Currently, all customization must be done within the Unity editor. Select the file "Assets/Prefabs/GameManager.prefab" in the Unity Inspector. Exciting parameters to tweak include:

**Game Manager**
- Powerup Frequency (how often powerups occur)
- Powerup Variance (+/- value to affect Powerup Frequency; i.e. if Variance=0.5, then Frequency = 0.5 to 1.5)
- Points To Win

**Table Manager**
- Num Rows (# bricks width-wise; i.e. Num Rows = 2 in the default game)
- Num Cols (# bricks height-wise; i.e. Num Cols = 7 in the default game)
- Background Width (width of play area)
- Background Height (height of play area)

![Ingame Wide](/../screenshots/ingame-wide.png?raw=true "Example customization. Try this with sudden death (Points To Win = 1)!")
