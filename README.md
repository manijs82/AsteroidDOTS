# AstroidDOTS
 Futuregames assignment for the Computer Tech course
 This project is made fully using DOTS

 Target grade: VG

 ## How the implementation makes efficient use of computer hardware
 
 The game utilizes the DOTS package as much as possible. As a result all entities (mainly enemies) are just a data record and not an object that needs initialization which adds significante overhead to the hardware.
 This also removes the need for object pooling as Instansiating and Destroying entities is adding and removing data from a table.

 ## How the implementation uses a data-oriented method
 
 Thanks to the DOTS workflow all systems of the are developed in a data-oriented fashion
 - Enemies are just entities that contain a EnemyTag and a MovementData.
 - The Player is just an entity with a PlayerTag, PlayerInputData, MovementData and a LaserData.
 - Multiple system operate on the data contained within the existing entities
   - MovementSystem updates the LocalTransform of entities that have a MovementData
   - EnemyMovementSysetm updates the MovementData of enemy entities based on direction to the player
   - PlayerInputSystem updates the MovementData of the player based keyborad input
   - LaserCollisionSystem flags enemies colliding with the laser as dead
   - and many more systems

 ## How code was optimized by based on findings from using a profiler

- Very early in the project I discovered using ShpereColliders are much more efficient than using BoxColliders
- Converting LaserCollisionSystem to utilize jobs and the burst complier to calculate the collision math improved performace of the system (shown in the November 2nd commits)
