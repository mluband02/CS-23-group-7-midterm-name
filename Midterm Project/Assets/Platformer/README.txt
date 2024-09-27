README

To start, try loading up "PlatformerTestScene" which should
be in the same folder as this README - you can hit play 
in the editor and control the character with arrows or wasd.

Next I'd recommend playing around with the settings of the 
variables of the "PlayerMovement" script attached to "PlayerBody",
or the "squashAndStretch" script attached to the "Player" object
which is a child of "PlayerBody." 

One of those parameters is the sprite in "PlayerBody" which you 
can replace with another image if you like, perhaps of your 
protagonist. By default it's a sqaure that has been tinted red
(you can change the color very easily in the sprite renderer)

The player controller will only be able to jump off of layers
which are represented in its "jumpable" layermask - this is
"Ground" by default (note the capital G) so make sure to put
objects which represent the ground on the Ground layer

If you want to get a better sense of _how_ they work under the hood,
of course feel free to look in the scripts themselves, I've tried
to comment them to explain what's going on. 

If you're having trouble understanding what's going on, feel free to
email me at ezraszanton@yahoo.com

Have fun!
-Ezra