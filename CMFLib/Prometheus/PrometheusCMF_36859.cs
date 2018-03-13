﻿using static CMFLib.CMFHandler;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_36859 : ICMFProvider {
        public byte[] Key(CMFHeader header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = 31;
            const uint increment = 393;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx -= increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.DataCount & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                switch (kidx % 3) {
                    case 0:
                        kidx += 103;
                        break;
                    case 1:
                        kidx = 4 * kidx % header.BuildVersion;
                        break;
                    case 2:
                        --kidx;
                        break;
                }

                buffer[i] ^= digest[(kidx + header.BuildVersion) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }
        
        private static readonly byte[] Keytable = {
            0xAB, 0x6B, 0xD4, 0xC1, 0x8E, 0xB5, 0xDE, 0xE3, 0xE7, 0xE0, 0x0C, 0x0B, 0x0E, 0xD2, 0x9D, 0x88, 
            0x6F, 0xD9, 0x6E, 0x7A, 0x0A, 0x17, 0x09, 0x75, 0x35, 0x1C, 0x95, 0x65, 0xDD, 0x02, 0xE2, 0x06, 
            0x49, 0xBF, 0xE6, 0xAD, 0x68, 0xB2, 0x6E, 0x53, 0x2D, 0x64, 0x68, 0x59, 0x18, 0x98, 0x6E, 0x63, 
            0x28, 0x65, 0x9E, 0xAF, 0x50, 0x71, 0x97, 0x0E, 0x35, 0xEA, 0x34, 0x8D, 0x24, 0x93, 0x26, 0xE6, 
            0xD5, 0x4B, 0xD9, 0xA4, 0x50, 0x44, 0x2B, 0x1E, 0x3E, 0x5F, 0x0C, 0xFE, 0xA3, 0x48, 0xE9, 0x30,
            0x5F, 0x51, 0x5E, 0x43, 0x9B, 0x12, 0x13, 0x7D, 0x8F, 0x75, 0x83, 0xFB, 0xB4, 0xE1, 0x03, 0x38, 
            0xCC, 0x6B, 0xCB, 0xFC, 0x86, 0x65, 0x2B, 0x2D, 0x73, 0xEC, 0x16, 0x9B, 0xA5, 0x32, 0x6E, 0x35, 
            0x6C, 0xA9, 0xE4, 0xF5, 0x96, 0x25, 0x39, 0x31, 0xEC, 0x33, 0x5F, 0x4E, 0x81, 0xFE, 0x4A, 0x25,
            0xCF, 0xA7, 0x05, 0xB7, 0x3F, 0xCC, 0x36, 0x68, 0xBD, 0x58, 0x7E, 0xA4, 0x2B, 0x1C, 0x2B, 0xDC, 
            0x47, 0x34, 0xDD, 0xFE, 0xE5, 0x17, 0x3A, 0x84, 0xDE, 0xAE, 0x20, 0x43, 0xFD, 0x42, 0x7B, 0x74, 
            0xE9, 0xFB, 0x7D, 0x85, 0x03, 0x28, 0x1F, 0xE6, 0xD9, 0x26, 0x07, 0xD1, 0x2F, 0x3A, 0xAF, 0x9D, 
            0xEC, 0xB9, 0x42, 0x70, 0x1E, 0x35, 0x7A, 0x08, 0xE1, 0x93, 0x25, 0xB5, 0x27, 0x34, 0xED, 0x04, 
            0x89, 0xA4, 0x87, 0xC6, 0xCD, 0x2A, 0x51, 0xB5, 0xCB, 0x5A, 0x70, 0x8C, 0x20, 0xF4, 0xD9, 0x90, 
            0xCA, 0xC1, 0x20, 0xC5, 0xDD, 0x68, 0x18, 0x03, 0x61, 0xF1, 0xE2, 0xE0, 0x53, 0xE0, 0x82, 0xDF, 
            0x31, 0x5C, 0x8A, 0x6D, 0x73, 0x67, 0xBD, 0xF0, 0xCE, 0x0D, 0x4F, 0xC2, 0xBF, 0x2A, 0x2F, 0x02, 
            0xF8, 0x16, 0xEC, 0xF3, 0x68, 0x34, 0xD1, 0xD9, 0x0B, 0xA6, 0xD0, 0x42, 0xAD, 0x11, 0x0A, 0x1A,
            0x96, 0x03, 0x95, 0x46, 0xB8, 0x63, 0xCE, 0xA1, 0xBE, 0x61, 0x05, 0x13, 0x06, 0xBD, 0x97, 0x3A, 
            0x3B, 0xC3, 0x6F, 0x76, 0xF2, 0xE8, 0xFC, 0x12, 0x37, 0x72, 0xCE, 0x72, 0xCC, 0xB9, 0xB3, 0x2D, 
            0x87, 0x2F, 0x21, 0x1B, 0x13, 0xE9, 0xC4, 0x34, 0x80, 0xE4, 0x18, 0x1B, 0x32, 0x7F, 0x05, 0x95,
            0x50, 0x85, 0x41, 0x3F, 0x2A, 0xD3, 0xDC, 0x60, 0x72, 0x01, 0x24, 0x62, 0xE0, 0xD4, 0x3A, 0x75, 
            0x35, 0xD3, 0xCE, 0xF8, 0x1E, 0x2E, 0x16, 0x99, 0x49, 0x19, 0x4B, 0xFA, 0x53, 0x75, 0x04, 0xFE, 
            0x4E, 0xDD, 0x3C, 0x79, 0x79, 0xEB, 0xEF, 0x76, 0x58, 0x10, 0x17, 0x55, 0x76, 0xA9, 0x23, 0x6B, 
            0x93, 0xB3, 0x67, 0xCF, 0x45, 0xDF, 0xF8, 0xBF, 0x98, 0xF6, 0xD3, 0x3E, 0x84, 0xD0, 0xF4, 0x94, 
            0xC5, 0xD5, 0x22, 0x38, 0xBB, 0x61, 0xF3, 0xC4, 0x4E, 0x5F, 0x60, 0x2C, 0xC9, 0x8C, 0xD3, 0xBC, 
            0x9D, 0xE8, 0xD9, 0x98, 0x8E, 0xE2, 0xE2, 0x7D, 0x90, 0x1F, 0x15, 0x60, 0x0F, 0x47, 0xB3, 0x79, 
            0xF9, 0x2C, 0x75, 0xDA, 0x34, 0x7D, 0xE6, 0xE1, 0xBB, 0xD9, 0xBB, 0xB0, 0x89, 0x2E, 0x74, 0xD7, 
            0xA4, 0x52, 0xEC, 0x8D, 0xFC, 0xBD, 0xA7, 0xC0, 0xB4, 0x7C, 0xA9, 0x3E, 0x68, 0x34, 0x95, 0xE2,
            0x65, 0x37, 0x0E, 0x0F, 0x32, 0x48, 0x9C, 0x4E, 0xF4, 0xD7, 0xC2, 0x04, 0xCF, 0x7B, 0x4E, 0xA9, 
            0x20, 0x6C, 0xA4, 0xF0, 0xDE, 0xC4, 0x8C, 0x1F, 0xD2, 0xBA, 0x63, 0x77, 0xDD, 0x2C, 0xE2, 0xBD, 
            0xC9, 0x19, 0x9F, 0x00, 0x41, 0xC5, 0xCF, 0x39, 0x93, 0x13, 0x74, 0x93, 0xE4, 0xAA, 0xEF, 0xC2,
            0xDE, 0x74, 0xE3, 0xA5, 0x5E, 0xAB, 0x44, 0x73, 0x9B, 0x46, 0xD5, 0x49, 0xE6, 0xBD, 0xB4, 0x95, 
            0xF9, 0xF3,  0x64, 0x75, 0x81, 0x0F, 0x22, 0xC5, 0x8A, 0x86, 0xFE, 0xC3, 0x78, 0x4B, 0x74, 0x4B
        };
    }
}