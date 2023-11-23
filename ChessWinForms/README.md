# SachyWF
1) Launching:
Launch the application with "Launch Potato Chess.lnk" or navigate to bin/Release/Sachy_Obrazky.

2) Controls:

Application is controlled purely by buttons. Description follows:

3) Buttons

a) Board buttons

Each button on board represents one square. Clicking a button can results in different actions:
- light or dark background: If a piece belongs to the currently moving player, 
    highlights the piece and all squares where this piece is allowed to move with green color. Otherwise does nothing.
- highlighted square: If an empty square or opponent's piece, player makes a move on this position with currently highlighted piece. 
    If a player clicked on highlighed piece, de-highlights all squares (and can choose different piece to move with).

If the game ends, all board buttons get disabled.

b) Promotion buttons

These buttons only show and get enabled when user orders pawn to move on the last rank. The buttons show pieces which pawn can promote to (queen, rook, bishop, knight).
The color of these pieces is the same as color of the pawn. After a piece is selected, the move with promotion is made, and the buttons get hidden again.

c) Flip button

Flips the board by 180 degrees by adjusting board button locations and coordinates appropriately.
The button also shows which player perspective are we watching ("white" means that the rank 1 is at bottom of the board).

d) Save notation button

The current game is temporarily saved in a file "partie.txt".
Clicking this button will rewrite the notation into a file "prepsana.txt" in a changed format. Such format was accepted
by page http://www.caissa.com/chess-tools/pgn-editor.php .
By copying notation into the page (select "Insert PGN file") you can get full PGN format.

e) New game and difficulty buttons

The button "New game" creates a new game with the parameters that are currently shown on the button:
AI vs AI, Player vs Player, AI vs Player, Player vs AI . The first parameter is for white side, the second one for black side.
The number following AI is the default depth search.
In order to change the parameters, click on buttons above the NewGame button:

"Player" will set human player as white/black.
"AI" will set AI with the currently showing depth as white/black.
Buttons +/- will change the depth for the next time you click on the AI buttons (text changes accordingly)

4) TextBoxes

a) Coordinates

Rank coordinates are at the left side of the board. In case of white's perspective, they are ordered 8->1 (top to bottom).
File coordinates are below the board. In case of white's perspective, they are ordered a->h (left to right).

b) Game end box

This box only shows when game ends (mate, stalemate, insufficient material, 50 moves without moving pawn or capturing piece) with the according result.

c) Computer analysis box

This box is permanently enabled. After computer makes a move, it will show principal variation - expected best move sequence
and evaluation of the final position (from its perspective).
WinForms
