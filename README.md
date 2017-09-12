# README #
Copyright 2017,  Gamegineer Corp.

Core API's for Word Search game

### What is this repository for? ###

* Parsing 2 files: a word search board and a dictionary (newline delimited)
* Core search algorithms:
** Dictionary stored in tree via per letter with bool denoting if letter path is a word at this node.  Helps disqualify word paths faster.
** A board walker with 8 potential directions.  Letters only used once and board edge does not wrap.

### How do I get set up? ###

* wordsearch <board, dictionary>


### Who do I talk to? ###

* chuck@gamegineer.com