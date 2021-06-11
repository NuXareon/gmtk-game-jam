# gmtk-game-jam
Buff Frog

Name: Cupid         
Genre: RTS Puzzle
Top down (ish) 3d view

** = optional / tbd

! = critical / MVP

#### Conecting people:
* ! Player connects pairs of people by drag and drop, this will make the people interact.
* ! Interaction people are attracted to each other, this will make them either:
   * Both of them move
   * Only one of them move
* ! Connected people keep moving towards each other until they meet or hit an obstacle, once they meet they fall in love
* ! The player wins when the target person falls in love with any other person.
* ! Other person can fall in love between themselves, once they fall in love they can't be selected anymore
* ** The target might already have a defined route (interaction) at the start of the level **
* ** Person can be connected to objects **
* ** The target needs to meet an specific partner **

#### Disconnecting people
* ** Second type of arrows wich repel people from each other **

#### Obstacles
* There are lethal and non-lethal obstacles:
    * Lethal obstacles will eliminate a person who collides with them
    * ! Non-lethal obstacles will terminate the interaction uppon being touch
* Obstacles can be permanent or deactivate after being triggered
* Some obstacles can be either:
    * ! Static, e.g. a wall.
    * Dynamic, e.g. a moving car.

#### Lost conditions
* ! There is a limited amount of connections that can be made. When the limit of connnections is reached and all pending interactions resolve, if the target hasn't found a partner the game is lost.
* If the target hits a lethal obstacle the game is lost.
* ! If there aren't any other people except the target the game is lost.

#### Misc
* ! Need to be able to restart the level


Prototype:
* Player connects pairs of people by drag and drop, this will make the people interact.
* Interaction people are attracted to each other.
* Connected players keep moving towards each other until they meet or hit and obstacle, once they meet they fall in love
* The player wins when the target person falls in love with any other person

* There are non-lethal obstacles
* Non-lethal obstacles will terminate the interaction uppon being touch
* Obstacles are static

* If there aren't any other people except the target the game is lost
* Counter of number of connections done
