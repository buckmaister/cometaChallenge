// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";
import "@openzeppelin/contracts/access/Ownable.sol";

contract nftcometa is ERC721URIStorage, Ownable {
    uint256 private tokenIdCounter;

    constructor(string memory name, string memory symbol)
    ERC721(name, symbol) 
    Ownable(msg.sender) 
    {
        tokenIdCounter = 0;
    }

    function mintNFT(address to, string memory tokenURI) external onlyOwner {
        uint256 tokenId = tokenIdCounter;
        _mint(to, tokenId);
        _setTokenURI(tokenId, tokenURI);
        tokenIdCounter++;
    }
}
