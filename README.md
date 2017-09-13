# README #
Copyright 2017,  Gamegineer Corp.

Core API's for Word Search game

### What is this repository for? ###

* Parsing 2 files: a word search board and a dictionary (newline delimited)
* Core search algorithms:
** Dictionary stored in tree via per letter with bool denoting if letter path is a word at this node.  Helps disqualify word paths faster.
** A board walker with 8 potential directions.  Letters only used once and board edge does not wrap.

### How do I get set up? ###

* WordSearchAPI.exe words=Data/twl.txt board=Data/4x4.txt 
* words=XXX arg is required.  Supplied the lastest scrabble valid word list from SourceForge
* A randomized 6x6 board will generate if arg board=XXX is missing.


### Who do I talk to? ###

* chuck@gamegineer.com