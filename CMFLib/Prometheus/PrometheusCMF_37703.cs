﻿using System.Security.AccessControl;
using static CMFLib.CMFHandler;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_37703 : ICMFProvider {
        public byte[] Key(CMFHeader header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Constrain(length * header.BuildVersion);
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += header.EntryCount;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            for (int i = 0; i != length; ++i) buffer[i] = 0;

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x62, 0x3A, 0x4A, 0xDE, 0xF1, 0x3A, 0x80, 0xF1, 0xFE, 0x4B, 0xBB, 0xBD, 0x4C, 0x27, 0x90, 0x9C,
            0xB4, 0x29, 0xD2, 0xC7, 0x8B, 0x11, 0xEA, 0x53, 0x85, 0xA8, 0x32, 0x38, 0x38, 0xBB, 0x56, 0xB7,
            0x52, 0xD3, 0xD6, 0xBF, 0xC0, 0x75, 0x1B, 0xA2, 0xD9, 0xEF, 0x29, 0x3C, 0x3D, 0x8A, 0xC4, 0xCC,
            0x2F, 0x6D, 0x8C, 0x34, 0xA5, 0x32, 0xDC, 0x1E, 0x12, 0xC8, 0x89, 0x8D, 0xB4, 0x03, 0xDA, 0x11,
            0x17, 0x9F, 0xDC, 0x49, 0x30, 0xEB, 0x95, 0x0E, 0x0E, 0xC0, 0x95, 0xAD, 0xF7, 0x72, 0x22, 0x02,
            0xA3, 0x31, 0x15, 0x41, 0x5D, 0x5B, 0x9A, 0x45, 0xEB, 0x4B, 0x7E, 0x78, 0x86, 0xA1, 0xD3, 0xF2,
            0x1D, 0x39, 0xC8, 0x76, 0x7C, 0x17, 0xEC, 0xC3, 0x5B, 0x38, 0x9E, 0xF7, 0xCE, 0x05, 0x63, 0x8B,
            0x6A, 0x53, 0xDE, 0xCC, 0x25, 0x63, 0xC9, 0x4C, 0x49, 0xDB, 0x60, 0x7D, 0xB6, 0x44, 0xCF, 0xE3,
            0xB2, 0x53, 0xB1, 0xBF, 0x03, 0xE6, 0x65, 0x26, 0x1E, 0x23, 0xB3, 0x23, 0x4F, 0xFE, 0x3E, 0xD9,
            0xBB, 0xE4, 0x13, 0x73, 0x56, 0x2E, 0x83, 0xB8, 0x02, 0x6B, 0x1C, 0x4E, 0x8D, 0x9C, 0x86, 0x22,
            0x1B, 0x52, 0x43, 0x0E, 0x1D, 0xC0, 0xC2, 0x7B, 0x15, 0xE5, 0x4E, 0xE8, 0x68, 0xA9, 0x92, 0xC5,
            0x86, 0x2E, 0x1F, 0x3E, 0xD2, 0xB6, 0x52, 0xCC, 0xA6, 0xB4, 0x42, 0x42, 0x21, 0xD6, 0x96, 0x74,
            0x60, 0xB8, 0xF4, 0x21, 0xD0, 0xD3, 0x07, 0xB7, 0x95, 0xD9, 0xE8, 0xF8, 0x8D, 0xBE, 0x26, 0x66,
            0x6B, 0x7E, 0xE6, 0xC0, 0x93, 0x33, 0xF7, 0x18, 0xC1, 0xF9, 0xB4, 0x5F, 0xC6, 0x77, 0xB4, 0x21,
            0x77, 0x49, 0x8C, 0x1C, 0x10, 0x3F, 0x6A, 0xC6, 0xF9, 0xAC, 0x16, 0x46, 0x1B, 0x2E, 0x39, 0x8B,
            0xB7, 0xEA, 0xCF, 0x20, 0x6C, 0x4E, 0xB0, 0x95, 0x5B, 0xF9, 0x12, 0x57, 0x3C, 0x40, 0x08, 0x55,
            0x2A, 0x35, 0xC2, 0x17, 0x2F, 0x83, 0x92, 0xCD, 0x10, 0x9F, 0x7C, 0x98, 0x23, 0x46, 0xA6, 0xF3,
            0x91, 0x44, 0x33, 0xCF, 0x9A, 0xE0, 0x04, 0x4A, 0x17, 0xC4, 0x6A, 0xA3, 0xF1, 0x45, 0x22, 0x66,
            0x0D, 0xF7, 0x4A, 0xD9, 0x0C, 0xD2, 0xE2, 0x44, 0xD0, 0xF4, 0xD2, 0x8E, 0x02, 0x59, 0xF8, 0xB6,
            0x22, 0xD7, 0x12, 0x07, 0xA2, 0x44, 0x45, 0x63, 0x44, 0x5D, 0x54, 0x0B, 0x0A, 0xD0, 0x94, 0x71,
            0x25, 0xBF, 0xA4, 0x67, 0x4F, 0xC8, 0xB5, 0xAF, 0x5F, 0xF3, 0x42, 0x97, 0x04, 0xE2, 0x74, 0xEF,
            0xEA, 0x35, 0x7F, 0x75, 0x05, 0xF9, 0x06, 0x54, 0x23, 0xEF, 0x8D, 0x1C, 0x84, 0xF0, 0x0F, 0x67,
            0x1E, 0x2F, 0x05, 0xA4, 0xDA, 0x44, 0x95, 0xBF, 0xAA, 0x1B, 0x38, 0xEF, 0xE0, 0x18, 0x60, 0xA8,
            0x82, 0xC0, 0xE4, 0x8E, 0x89, 0x0D, 0x11, 0x48, 0xB5, 0x71, 0x4E, 0x33, 0x4C, 0xF9, 0xE6, 0xD7,
            0x1C, 0xEF, 0x73, 0x31, 0x64, 0x5F, 0x3D, 0x6A, 0x97, 0xB1, 0x01, 0xB2, 0x10, 0x7B, 0x24, 0xB7,
            0x18, 0x86, 0x76, 0xCD, 0x6B, 0x4B, 0x6C, 0x7B, 0x43, 0x04, 0x6F, 0x94, 0xFC, 0x3D, 0x36, 0xEE,
            0x3A, 0x32, 0xF9, 0xDE, 0x00, 0x70, 0x29, 0xB4, 0xA7, 0xDA, 0xD3, 0xC7, 0x2D, 0x79, 0xC3, 0xBB,
            0x5B, 0x13, 0xFE, 0xA4, 0x36, 0xF4, 0x21, 0x9B, 0x62, 0x85, 0x6A, 0x0F, 0x65, 0xB4, 0x00, 0x6C,
            0x48, 0x68, 0xA1, 0x49, 0x07, 0x84, 0x5A, 0xD4, 0x94, 0x2B, 0x79, 0xB9, 0x46, 0x5C, 0xD3, 0x2D,
            0x3E, 0x34, 0xA8, 0x15, 0x61, 0x24, 0x42, 0x62, 0x47, 0xF9, 0x38, 0x9D, 0x60, 0x26, 0xCC, 0x23,
            0x2C, 0xDD, 0x4D, 0x23, 0x69, 0x5B, 0x1B, 0x7D, 0x86, 0x55, 0xDE, 0x04, 0xD4, 0xD6, 0x44, 0xF8,
            0x74, 0xA7, 0x9C, 0x92, 0xE9, 0xE7, 0x23, 0x33, 0xB3, 0xB1, 0x71, 0x2D, 0x6D, 0x73, 0x0B, 0xB9
        };
    }
}