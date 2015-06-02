#include <iostream>  
#include <sstream>  
#include <string>  
#include <iomanip>  
#include <stdio.h>  
#include <openssl/sha.h>  
  
//#define _CRT_SECURE_NO_WARNINGS  
#pragma warning(disable : 4996)
  
std::string sha256( const std::string str )  
{  
    unsigned char hash[SHA256_DIGEST_LENGTH];  
    SHA256_CTX sha256;  
    SHA256_Init(&sha256);  
    SHA256_Update(&sha256, str.c_str(), str.size());  
      
    SHA256_Final(hash, &sha256);  
      
    std::stringstream ss;  
    for(int i = 0; i < SHA256_DIGEST_LENGTH; i++)  
    {  
        ss << std::hex << std::setw(2) << std::setfill('0') << (int)hash[i];  
    }  
     
    return ss.str();  
}  
  
void sha256_hash_string (unsigned char hash[SHA256_DIGEST_LENGTH], char outputBuffer[65])  
{  
    int i = 0;  
  
    for(i = 0; i < SHA256_DIGEST_LENGTH; i++)  
    {  
        sprintf(outputBuffer + (i * 2), "%02x", hash[i]);  
    }  
  
    outputBuffer[64] = 0;  
}  
  
int sha256_file(char* path, char output[65])  
{  
    FILE* file = fopen(path, "rb");  
    if(!file) return -1;  
  
   unsigned  char hash[SHA256_DIGEST_LENGTH];  
    SHA256_CTX sha256;  
    SHA256_Init(&sha256);  
    const int bufSize = 32768;  
    char* buffer = new char[ bufSize ];  
    int bytesRead = 0;  
    if(!buffer) return -1;  
    while((bytesRead = fread(buffer, 1, bufSize, file)))  
    {  
        SHA256_Update(&sha256, buffer, bytesRead);  
    }  
    SHA256_Final(hash, &sha256);  
  
    sha256_hash_string(hash, output);  
    fclose(file);  
    delete [] buffer;  
    return 0;  
}        
  
int main()  
{  
    // hash a string  
    std::cout << "SHA-256 hash of \"Sample String\" text:" << std::endl;   
    std::cout << sha256( "Sample String" ) << std::endl << std::endl;   
  
    // hash a file  
    /*std::cout << "SHA-256 hash of text file Hamlet.txt:" << std::endl;  
    char calc_hash[65];  
    sha256_file( "Hamlet.txt", calc_hash );  
    std::cout << calc_hash << std::endl;   */
  
    return 0;  
}  