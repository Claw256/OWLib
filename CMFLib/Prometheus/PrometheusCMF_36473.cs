﻿using static CMFLib.CMFHandler;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_36473 : ICMFProvider {
        public byte[] Key(CMFHeader header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Constrain(header.BuildVersion * length);
            uint increment = header.BuildVersion;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx = increment - kidx;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.BuildVersion & 511];
            uint increment = kidx % 29;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
                buffer[i] ^= (byte) ((digest[(kidx + header.EntryCount) % SHA1_DIGESTSIZE] + 1) % 0xFF);
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x9E, 0x8C, 0x4E, 0xAE, 0xE9, 0xC9, 0x80, 0x1C, 0x79, 0x27, 0xCD, 0x28, 0x69, 0xFB, 0x97, 0x73, 
            0xA2, 0x96, 0x71, 0x74, 0x65, 0x3D, 0xD6, 0x80, 0x24, 0x26, 0xB9, 0x3D, 0xE6, 0x59, 0xFD, 0xE3, 
            0x98, 0x15, 0x8A, 0x83, 0x06, 0x92, 0x82, 0x01, 0x87, 0x19, 0x19, 0xF9, 0x59, 0xDE, 0xFC, 0x91, 
            0xDC, 0x89, 0x72, 0x29, 0xF0, 0x01, 0x75, 0x04, 0xDE, 0x57, 0xC5, 0xDE, 0x24, 0x94, 0x9C, 0xE1, 
            0x85, 0xDB, 0x66, 0x36, 0xD5, 0xFA, 0x52, 0x80, 0x7F, 0xF8, 0xDA, 0x70, 0xA4, 0x1C, 0x40, 0x8C,
            0x2E, 0xDA, 0xD4, 0xE3, 0x47, 0x89, 0xE9, 0x54, 0x42, 0x06, 0x4C, 0x4E, 0xA8, 0xA4, 0x00, 0xCF, 
            0xB3, 0xE3, 0x66, 0x69, 0xB7, 0x26, 0xE7, 0x66, 0xAB, 0x72, 0xFA, 0x59, 0xC2, 0xE2, 0xBE, 0xFF, 
            0xC5, 0x8A, 0xCF, 0x86, 0x95, 0xDD, 0x45, 0x1E, 0x9E, 0x65, 0x38, 0xC4, 0xF2, 0xD5, 0x30, 0x3D,
            0x56, 0x3B, 0x4C, 0x1F, 0xAA, 0x8C, 0xF2, 0x79, 0x56, 0x28, 0xD2, 0x39, 0xD9, 0xA1, 0xAE, 0x2C, 
            0x37, 0x89, 0x19, 0xAB, 0xC6, 0x30, 0xAA, 0x0B, 0x56, 0xED, 0xDF, 0x2E, 0x3D, 0x13, 0x01, 0x58, 
            0xB5, 0x8A, 0x92, 0x37, 0x31, 0xCB, 0x6C, 0x3B, 0x99, 0x5B, 0x8D, 0x9D, 0x55, 0xFF, 0xC2, 0xA5, 
            0x3A, 0x33, 0xD3, 0xF1, 0x10, 0xD3, 0xBA, 0xDE, 0x91, 0x16, 0x6A, 0xFC, 0x7B, 0x70, 0xA6, 0x32, 
            0x61, 0x39, 0x8E, 0x59, 0x24, 0x01, 0xF6, 0x3A, 0x37, 0x9D, 0xEE, 0x34, 0x06, 0x7C, 0x2C, 0x08, 
            0xFA, 0xA2, 0x30, 0x6A, 0x1A, 0xFF, 0x14, 0x04, 0xAA, 0xCD, 0x2E, 0x89, 0x97, 0xB9, 0x8E, 0xDA, 
            0x27, 0xCF, 0xC0, 0x76, 0xA8, 0xC2, 0x8C, 0xF6, 0xF2, 0x6F, 0xB6, 0xA4, 0x1D, 0xB0, 0x8A, 0xFE, 
            0xCA, 0xC5, 0xB7, 0xCA, 0x79, 0x4A, 0x57, 0x5B, 0x3D, 0x2C, 0xD8, 0x7D, 0x7B, 0xED, 0x89, 0x64,
            0xBC, 0x27, 0xEE, 0x8E, 0x1C, 0x72, 0xE0, 0x80, 0xF0, 0x38, 0xC0, 0x88, 0xB0, 0x59, 0xA5, 0xE0, 
            0x0D, 0x7F, 0x0D, 0x60, 0x5C, 0x61, 0xA8, 0x05, 0x77, 0x38, 0xE0, 0xC5, 0x96, 0xB7, 0xF8, 0x7C, 
            0x17, 0x2C, 0x9D, 0xFD, 0x8F, 0x7F, 0xC2, 0x29, 0x7A, 0xE7, 0x90, 0x35, 0xBA, 0xFD, 0x33, 0x6F,
            0xC3, 0xD8, 0xE6, 0x41, 0x40, 0x67, 0x4B, 0xAE, 0x88, 0x92, 0xAE, 0x6E, 0xBD, 0x75, 0x37, 0x85, 
            0xA8, 0xE0, 0xD5, 0x9D, 0x10, 0xB2, 0x78, 0xBA, 0xEB, 0xBC, 0x7D, 0xBC, 0x81, 0xE7, 0xED, 0x68, 
            0x39, 0x81, 0x3B, 0x4C, 0xA5, 0x51, 0x93, 0x2E, 0xAE, 0x77, 0x1D, 0x70, 0xD6, 0x37, 0x0F, 0xCC, 
            0xDC, 0xAF, 0x13, 0x6B, 0xDD, 0x50, 0x32, 0xAF, 0x0C, 0x64, 0x6E, 0x18, 0xC1, 0x56, 0x95, 0x29, 
            0x8A, 0x7E, 0x3D, 0xC9, 0x02, 0xD6, 0x1F, 0xF8, 0x3C, 0x51, 0xB0, 0x36, 0x81, 0xD2, 0x84, 0x55, 
            0xEA, 0x02, 0xB3, 0x83, 0xF1, 0x80, 0x00, 0x43, 0x53, 0xF7, 0x08, 0x15, 0x14, 0xCF, 0xE3, 0xEA, 
            0x8C, 0x94, 0x08, 0x06, 0x36, 0xA0, 0xD0, 0x72, 0x1E, 0x8D, 0xEC, 0x67, 0xB3, 0xD4, 0x2C, 0x5F, 
            0x1A, 0xC1, 0x5D, 0x45, 0x25, 0x55, 0x84, 0x99, 0x26, 0x33, 0xD5, 0x48, 0xC5, 0x9D, 0x27, 0xEE,
            0x29, 0xCD, 0x80, 0xC3, 0x32, 0x67, 0xAE, 0x5F, 0x60, 0x97, 0x18, 0x74, 0x06, 0x45, 0x3A, 0x6B, 
            0x82, 0xF9, 0x40, 0xBC, 0x31, 0xA7, 0x3E, 0x8B, 0xA5, 0xAC, 0xAB, 0x2B, 0x97, 0x7A, 0x57, 0x62, 
            0x0D, 0xAE, 0xE0, 0x02, 0xE8, 0xFE, 0x61, 0xD0, 0xDB, 0xC5, 0x6C, 0x5E, 0x43, 0xDA, 0x6D, 0xC5,
            0x68, 0x38, 0xE0, 0x41, 0xA5, 0xF9, 0xAA, 0xD9, 0x54, 0x5D, 0xA2, 0xD8, 0xB1, 0xCF, 0x02, 0xE3, 
            0xF1, 0x43, 0x6B, 0xB0, 0x6E, 0x98, 0x35, 0xAE, 0x33, 0xA4, 0x6E, 0x3E, 0x2E, 0x02, 0x5B, 0xED
        };
    }
}