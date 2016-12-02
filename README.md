![Ingame Default](/../screenshots/ingame-default.png?raw=true "Default setup.")

What is PongOut?
==========

PongOut is a hybrid between the classics Pong and Breakout, with the added fun of *powerups*!


How to Run
==========

[Download the latest release for your operating system.](https://github.com/kevin-d-omara/PongOut/releases/latest "https://github.com/kevin-d-omara/PongOut/releases/latest") Then unzip the file and enjoy!

Instructions:
==========
- Players each control a paddle which moves up and down. Use your paddle to
deflect the ball towards your opponent. You score a point each time the ball
goes off your opponent's table edge.
- Players also own a set of bricks. These are your first line of defense from
letting the ball off your table edge. A brick is destroyed each time it is
struck by the ball.
- Roughly every 5 seconds a random powerup is activated, including:

  - **Rainbow** - ball color is randomized
  - **Jink** - ball dashes up or down
  - **Boost** - ball boosts forward
  - **Retro Boost** - ball boosts in reverse

- **Hint:** you can direct the ball's rebound angle based on it's distance from the
center of your paddle. The further away from the center, the wider the rebound
angle.

*First to 10 points wins!*

Controls:
==========
##### Player 1:

    Move Up - 'W'
    Move Down - 'S'

##### Player 2:

    Move Up - '↑'
    Move Down - '↓'
    
##### Pause

    'escape'
    'enter/return'
    'space bar'
    'p'

Customization:
==========
Currently, all customization must be done within the Unity editor. Select the file "Assets/Prefabs/GameManager.prefab" in the Unity Inspector. Exciting parameters to tweak include:

**Game Manager**
- Powerup Period (how often powerups occur; default 5 seconds)
- Powerup Variance (+/- value to affect Powerup Period; i.e. if Variance=0.5, then Period = 0.5 to 1.5; default 1 second)
- Points To Win (default 10 points)

**Table Manager**
- Num Rows (# bricks width-wise; i.e. default 2 rows of bricks)
- Num Cols (# bricks height-wise; i.e. default 7 bricks per row)
- Background Width (width of play area; default 10.24 Unity units)
- Background Height (height of play area; default 6.2 Unity units)

![Ingame Wide](/../screenshots/ingame-wide.png?raw=true "Example customization. Try this with sudden death (Points To Win = 1)!")
