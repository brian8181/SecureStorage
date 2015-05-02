// SecureDataCPP.cpp : Defines the entry point for the console application.
#include "stdafx.h"
#include <iostream>
#include "aes.h"
#include "osrng.h"

using CryptoPP::AutoSeededRandomPool;
using CryptoPP::AES;

int _tmain(int argc, _TCHAR* argv[])
{
	// gen key
	AutoSeededRandomPool prng;
	byte key[AES::DEFAULT_KEYLENGTH];
	prng.GenerateBlock(key, sizeof(key));
	
	int s = AES::DEFAULT_KEYLENGTH;
	AES::Encryption aesEncryption(key, AES::DEFAULT_KEYLENGTH);

	std::cout << "Press any key ..." << std::endl;
	char c;
	std::cin >> c;

	return 0;
}

