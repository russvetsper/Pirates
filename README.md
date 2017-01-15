## _Pirates _
#### By _**Russ Vetsper**_




## Description

The Pirate-Ships app lets ships captain keep track of pirates on ship , user can add , delete and match ships to pirates.

** To run this app you will need to clone this repository. On your computers, in PowerShell run "DNU restore" , Enter "DNX Kestrel" , then  Navigate in your browser to "LocalHost:5004" to enter the app.

** Creat database and tables:
* CREATE DATABASE pirates;
* GO
* USE pirates;
* GO
* CREATE TABLE ships (id INT IDENTITY(1,1), name VARCHAR(255), shiptype VARCHAR(255));
* CREATE TABLE pirates (id INT IDENTITY(1,1), name VARCHAR(255), rank VARCHAR(255));
* CREATE TABLE pirates_ships (id INT IDENTITY(1,1), ships_id INT, pirates_id INT);
* GO

## Specs

| Behavior     | Input  |Output Example  |
| ------------- |:-----:|:----------: |
| check for empty database | none | true |
| User adds a ship| name of ship: "Black Pearl" |ship "Black Pearl" is on list for ships  |
| User edits a ship | edit ship "Black Pearl" for ship " Flying Dutchman" | ship name updated to " Flying Dutchman"|
| User deletes ship | Delete "Flying Dutchman"  | ship " Flying Dutchman" deleted |
| User adds a pirate to a ship | new pirate "Black Beard" added to ship | "Black Beard" is added to ship  |
| User edits a pirate | edit pirate "Black Beard" to pirate "Davy Jones" |pirates name updated to "Davy Jones"
|User deletes pirate| delete pirate "Davy Jones"| "Davy Jones" deleted ,no name in ship list|



## Known Bugs


None

## Technologies Used
 C#, Nancy, Razor, HTML, CSS, Bootstrap, Xunit Testing, SQL

### License
Copyright (c) 2016 _**Russ Vetsper **_
