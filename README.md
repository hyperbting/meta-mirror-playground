

## Execution Order in Mirror Networked

### HOST(Server+Client)

##### NetworkedPlayer Awake 
isLocalPlayer: $${\color{red}False}$$, isClient: $${\color{red}False}$$
isServer: $${\color{red}False}$$, isOwned: $${\color{red}False}$$

##### NetworkedPlayer OnEnable 
isLocalPlayer: $${\color{red}False}$$, isClient: $${\color{red}False}$$
isServer: $${\color{red}False}$$, isOwned: $${\color{red}False}$$

##### NetworkedPlayer Start (ONLY timing seems show correct state
isLocalPlayer: $${\color{green}True}$$, isClient: $${\color{green}True}$$
isServer: $${\color{green}True}$$, isOwned: $${\color{green}True}$$

##### NetworkedPlayer OnDisable 
isLocalPlayer: $${\color{red}False}$$, isClient: $${\color{red}False}$$
isServer:  $${\color{red}False}$$, isOwned: $${\color{red}False}$$

##### NetworkedPlayer OnDestroy 
isLocalPlayer: $${\color{red}False}$$, isClient: $${\color{red}False}$$
isServer: $${\color{red}False}$$, isOwned: $${\color{red}False}$$

