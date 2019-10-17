Projekt Struktur
================

**Note : Es ist ein Projekt der Uni Tübingen. App ist Teil der Bachelorarbeit in WS 19/20. Betreuung durch Kevin Körner.


Wie der Name schon sagt, hier wird der Projektbaum (Projektstruktur) dargestellt:
```
+---------------+
+ TuebiQuestApp +----+
+---------------+    |    
                 +---v----+
                 | Assets +-------+
                 +---v----+       |
                          |  +----v----+
                          |  |Resources|
                          |  +---------+
                          +--------+
                          |        |
                          |  +-----v------+
                          |  |TextMesh Pro|
                          |  +------------+
                          +---------+
                                    |
                             +------v-------+
                             |TuebiQuestApp +-------+
                             +--------------+       |
                             |              |   +---v--+
                             |              |   | docs |
                             |              |   +------+
                             |              |
                             |              +---------+
                             |              |         |
                             |              |   +-----v----+
                             |              |   | Graphics +-------------------------+
                             |              |   +----------+                         |
                             |              |              |                   +-----v------+
                             |              |              |                   | Animations |   
                             |              |              |                   +------------+
                             |              |              +-------------------------+
                             |              |              |                         |
                             |              |              |                   +-----v---+
                             |              |              |                   | Prefabs |   
                             |              |              |                   +---------+
                             |              |              +-------------------------------------------------+
                             |              |                                                                |
                             |              |                                                          +-----v-----+
                             +--------+     +--------------------------------+                         | Materials +-----------+
                                      |                                      |                         +-----------+           |
                                 +----v----+                            +----v---+                                 |   +-------v-------+
                                 | Scripts +-----------+                | Scenes +-----------+                     |   | BearsShooting |
                                 +---------+           |                +--------+           |                     |   +---------------+
                                           |   +-------v-------+                 |   +-------v-------+             +-----------+
                                           |   | BearsShooting |                 |   | BearsShooting |             |           |
                                           |   +---------------+                 |   +---------------+             |   +-------v------+
                                           |                                     |                                 |   | BoatsSinking |
                                           +-----------+                         +-----------+                     |   +--------------+
                                           |           |                         |           |                     +-----------+
                                           |   +-------v------+                  |   +-------v------+              |           |
                                           |   | BoatsSinking |                  |   | BoatsSinking |              |   +-------v--+
                                           |   +--------------+                  |   +--------------+              |   | Eberhard |
                                           +-----------+                         +-----------+                     |   +----------+
                                           |           |                         |           |                     +-----------+
                                           |   +-------v--+                      |   +-------v--+                  |           |
                                           |   | Eberhard |                      |   | Eberhard |                  |   +-------v--------+
                                           |   +----------+                      |   +----------+                  |   | FlowerPlucking |
                                           +-----------+                         +-----------+                     |   +----------------+
                                           |           |                         |           |                     +-------+
                                           |   +-------v--------+                |   +-------v--------+            |	   |
                                           |   | FlowerPlucking |                |   | FlowerPlucking |            |   +---v---+
                                           |   +----------------+                |   +----------------+            |   | Intro |
                                           +-------+                             +-------+                         |   +-------+
                                           |       |                             |       |                         +-----------+
                                           |   +---v---+                         |   +---v---+                     |           |
                                           |   | Intro |                         |   | Intro |                     |   +-------v------+
                                           |   +-------+                         |   +-------+                     |   | LustnauerTor |
                                           +-----------+                         +-----------+                     |   +--------------+
                                           |           |                         |           |                     +-----------+
                                           |   +-------v------+                  |   +-------v------+              |           |
                                           |   | LustnauerTor |                  |   | LustnauerTor |              |   +-------v------+
                                           |   +--------------+                  |   +--------------+              |   | RathausQuest |
                                           +-----------+                         +---------+                       |   +--------------+
                                           |           |                         |         |                       +--------+
                                           |   +-------v-------+                 |   +-----v-----+                 |        |
                                           |   | Questionnaire |                 |   | Minigames |                 |   +----v---+
                                           |   +---------------+                 |   +-----------+                 |   | Shared |
                                           +-----------+                         +-----------+                     |   ---------+
                                           |           |                         |           |                     +-----------+
                                           |   +-------v------+                  |   +-------v-------+                         |
                                           |   | RathausQuest |                  |   | Questionnaire |                 +-------v---+
                                           |   +--------------+                  |   +---------------+                 | WordsGame |
                                           +-------+                             +-----------+                         +-----------+
                                           |       |                             |           |   
                                           |   +---v---+                         |   +-------v------+		  
                                           |   | Utils |                         |   | RathausQuest |
                                           |   +-------+                         |   +--------------+ 
                                           +-----------+                         +-----------+
                                                       |                                     |
                                               +-------v---+                         +-------v---+
                                               | WordsGame |                         | WordsGame |
                                               +-----------+                         +-----------+

```

## TuebiQuestApp
Es ist der ROOT-Ordner des Projekts.

## docs
Ist ein Unterverzeichniss von TuebiQuestApp. Hier werden die wichtigste Dokumente bezüglich Projekts abgelegt.

## Graphics
Hier werden alle Grafiken abgelegt. Unter anderem Animationen, Prefabs und alle Bilder und Sprites. Bilder und Sprites gehören in den Materials Ordner. Der Ordner beinhaltet einzelne Minigames Ordner, wo entsprechende Grafiken abgelegt werden.

## Scenes
Hier werden einzelne Szenen in einzelne Ordner abgelegt. 

## Scripts
Hier werden alle Skripte abgelegt, die zu entsprechendem Mini-Spiel gehören.