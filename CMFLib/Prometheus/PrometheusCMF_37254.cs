﻿using static CMFLib.CMFHandler;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_37254 : ICMFProvider {
        public byte[] Key(CMFHeader header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[length + 256];
            const uint increment = 3;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Constrain(header.BuildVersion * length);
            uint increment = kidx % 29;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
                buffer[i] ^= (byte) ((digest[(kidx + header.EntryCount) % SHA1_DIGESTSIZE] + 1) % 0xFF);
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x09, 0xFA, 0x85, 0x3C, 0xBF, 0x30, 0xC4, 0xCF, 0x49, 0x6F, 0x6B, 0x6B, 0xC0, 0x3D, 0xD8, 0x4D, 
            0x79, 0x41, 0x0C, 0xFB, 0xDA, 0x03, 0x9F, 0x85, 0x99, 0xAB, 0x6B, 0xC8, 0x3C, 0xC1, 0x8F, 0x48, 
            0x75, 0x82, 0x33, 0x46, 0x2E, 0x1D, 0xC0, 0xE8, 0x8C, 0xEC, 0x5A, 0x9E, 0x9D, 0xE3, 0xBD, 0x5D, 
            0x85, 0x59, 0x17, 0x30, 0x3E, 0x8E, 0xD4, 0x64, 0x3E, 0xE2, 0x4E, 0xE3, 0x0D, 0x66, 0xCE, 0xAA, 
            0x4E, 0xF6, 0xBB, 0xED, 0xD8, 0x25, 0x3F, 0x30, 0xA7, 0xF8, 0x53, 0x1B, 0x22, 0x9B, 0x65, 0xFB,
            0x29, 0x33, 0x0F, 0x1A, 0x00, 0x74, 0x39, 0xBE, 0xD7, 0x7A, 0x3D, 0xC8, 0x25, 0x21, 0x25, 0x2F, 
            0xA9, 0xCE, 0x20, 0xCB, 0x4C, 0x57, 0x82, 0xEB, 0x7D, 0x72, 0xB5, 0x7F, 0x28, 0x49, 0xFC, 0x82, 
            0xAD, 0xC3, 0x75, 0xAF, 0xBB, 0x68, 0x9E, 0x57, 0x90, 0x77, 0x68, 0x43, 0xBF, 0x9E, 0x7C, 0xE6,
            0x73, 0x38, 0xE9, 0xF5, 0x92, 0x55, 0x9D, 0x8F, 0x32, 0xF2, 0xAA, 0xE3, 0x83, 0xDD, 0x6B, 0xCA, 
            0x6A, 0xDC, 0xA1, 0x5D, 0x1C, 0x14, 0x73, 0xCE, 0x7B, 0xB5, 0x1C, 0x4C, 0xB9, 0xEE, 0x12, 0xC4, 
            0x0F, 0x54, 0x11, 0x1A, 0x2F, 0x48, 0x06, 0xA6, 0x82, 0x50, 0xF7, 0xA3, 0xA3, 0x41, 0x99, 0xBE, 
            0x9E, 0x83, 0x50, 0xC1, 0x20, 0xCC, 0x98, 0xA2, 0xF2, 0x68, 0x9A, 0x48, 0x78, 0x02, 0xF4, 0x2C, 
            0x69, 0x16, 0x38, 0x82, 0x89, 0x67, 0x19, 0x70, 0x7F, 0x8B, 0x05, 0x30, 0x09, 0x32, 0x52, 0xB9, 
            0xF9, 0x47, 0x54, 0xDB, 0xA4, 0x11, 0x12, 0xA8, 0x15, 0x87, 0xD8, 0xFA, 0x61, 0x1E, 0x8C, 0x21, 
            0x77, 0x2A, 0x33, 0x6E, 0x7A, 0x88, 0x33, 0x6F, 0xAD, 0x39, 0x7A, 0x00, 0xDD, 0xA6, 0xC3, 0x8A, 
            0x35, 0xED, 0x9F, 0xF5, 0x48, 0x4C, 0xED, 0xB1, 0xA8, 0xCF, 0xFF, 0x65, 0x93, 0x1D, 0x31, 0xB0,
            0xCF, 0xCD, 0x6E, 0xDE, 0x83, 0x90, 0x78, 0x5A, 0x2F, 0x6D, 0xA0, 0x19, 0x95, 0x1F, 0x10, 0x3B, 
            0xDF, 0x43, 0x7C, 0x95, 0x5A, 0x23, 0x14, 0x6E, 0x8B, 0x5F, 0xCA, 0xBA, 0xAC, 0xFD, 0x54, 0x8A, 
            0xAC, 0x4E, 0x6D, 0x32, 0xB3, 0x76, 0x9E, 0xDA, 0xAC, 0xB5, 0x65, 0xB3, 0x68, 0x35, 0xD9, 0x13,
            0x69, 0x66, 0x4A, 0x40, 0x9C, 0xC0, 0x11, 0x46, 0xDE, 0x5D, 0x9F, 0x10, 0x80, 0xB3, 0xCB, 0xE3, 
            0xE9, 0x41, 0xBD, 0xE8, 0xF4, 0x41, 0xFA, 0x7D, 0x73, 0xAE, 0x40, 0xE1, 0x62, 0x3F, 0x9E, 0xDB, 
            0xDC, 0x1F, 0x45, 0x37, 0x51, 0x15, 0x1B, 0xF6, 0x07, 0x64, 0x3A, 0x45, 0x6B, 0x46, 0x0E, 0xFA, 
            0x76, 0xAC, 0x9E, 0x72, 0x7A, 0x79, 0xF0, 0xF7, 0xD1, 0x76, 0xB5, 0x94, 0x82, 0xFD, 0xBB, 0x13, 
            0xF0, 0xDA, 0x22, 0xCA, 0xB7, 0xC1, 0x69, 0x22, 0xC4, 0xA5, 0x57, 0x63, 0xCE, 0xDF, 0x6E, 0xBB, 
            0x56, 0x68, 0xC2, 0x21, 0x0C, 0x90, 0xCB, 0xC9, 0xF7, 0xF5, 0x22, 0x8F, 0xDF, 0x73, 0x9C, 0x85, 
            0x56, 0xE8, 0x7A, 0x63, 0x75, 0xF1, 0xD0, 0x4E, 0xB8, 0x02, 0x42, 0x52, 0xC1, 0xED, 0xE8, 0xA3, 
            0x02, 0xEA, 0x12, 0x2D, 0x26, 0xC1, 0x84, 0xC9, 0x51, 0xB0, 0x56, 0x59, 0xF8, 0x15, 0x44, 0x39,
            0x38, 0x5C, 0xB7, 0x18, 0x82, 0x98, 0xC7, 0x9B, 0xAB, 0xF6, 0x1F, 0x3F, 0x34, 0x53, 0xC6, 0x36, 
            0x83, 0x1D, 0x23, 0x76, 0xC6, 0x31, 0x6B, 0x84, 0x76, 0x4E, 0xFA, 0x4C, 0x32, 0xEA, 0x8A, 0xB1, 
            0xB4, 0x2D, 0x20, 0xDB, 0x38, 0x4D, 0xE5, 0xBD, 0x8D, 0x89, 0xE6, 0xC3, 0x94, 0xF7, 0x33, 0x0F,
            0x6A, 0x75, 0xF8, 0x3A, 0x06, 0xF7, 0x05, 0xD1, 0xA3, 0xEE, 0x53, 0x92, 0xAD, 0xD4, 0x7E, 0xC4, 
            0x6C, 0x7E, 0x42, 0x87, 0x8B, 0x46, 0x35, 0x4C, 0x0B, 0x71, 0xD3, 0x2D, 0xE0, 0x48, 0x51, 0x59
        };
    }
}