// OpenSSLConsolePOC.cpp : Defines the entry point for the console application.
//

//#include "stdafx.h"
#include <iostream>

#include <stdio.h>
#include <string.h>
#include <openssl/sha.h>

int main()
{
	unsigned char ibuf[] = "compute sha1";
    unsigned char obuf[20];

	//SHA_CTX sha1;
	//SHA1_Init(&sha1);
	
	//unsigned char hash[SHA256_DIGEST_LENGTH];  
    //SHA256_CTX sha256;  
    //SHA256_Init(&sha256);  
		 
    SHA1(ibuf, strlen((char*)ibuf), obuf);

    int i;
    for (i = 0; i < 20; i++) {
        printf("%02x ", obuf[i]);
    }
    printf("\n");

	return 0;
}

