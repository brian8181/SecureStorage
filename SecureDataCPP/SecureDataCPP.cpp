// SecureDataCPP.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "aes.h"

using CryptoPP::AES;

int _tmain(int argc, _TCHAR* argv[])
{
	byte* key = new byte[100];
	AES::Encryption aesEncryption(key, AES::DEFAULT_KEYLENGTH);
	delete [] key;
	return 0;
}

