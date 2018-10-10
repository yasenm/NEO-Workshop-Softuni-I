# NEO Workshop event in Softuni 09/10/18

This was an workshop event targeting hands on presentation for deloping smart contracts on NEO blockchain.

Additionally creating a live node connected to private network for tracking blockchain events.

# Please refer to this settings of local environment in order to build and run the projects

(I) Writing and compiling smart contracts

1. Get VS 2017 (we use community edition)
2. In VS
	- .Net Core cross-platform development needs to be installed
	- Install NeoContractPlugin
3. Install neo-compiler
	- From https://github.com/neo-project/neo-compiler clone the project
	- Publish Neon project
	- Add system environment path to the published folder
4. Neo gui https://github.com/neo-project/neo-gui - tool for wallets and interacting with the blockchain
5. Neo debugger for smart contracts - https://github.com/CityOfZion/neo-debugger-tools


(II) Local private-net setup


1. Download and install docker
	- Link https://docs.docker.com/docker-for-windows/install/
	- Look for guides if your windows is not Pro edition on how to install for Home edition
2.  Private net with docker compose consisting of neo-privatenet and neoscan
	- Clone project https://github.com/slipo/neo-scan-docker
	- Run `docker-compose up` in the project folder
	- Set in hosts file on your machine `127.0.0.1 neo-privnet`
	- When done check neoscan at localhost:4000
