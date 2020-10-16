using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitFlag {

    //符号なしの32ビット
    private uint bit;

    //フラグビットを取得
    public uint Bit
    {
        set { bit = value; }
        get { return bit; }
    }

    //指定したビットが立っているかチェック
    public bool CheckBit(uint targetBit) { return (bit & targetBit) == targetBit; }
    public static bool CheckBit(uint baseBit, uint targetBit) { return (baseBit & targetBit) == targetBit; }

    //指定したビットのどれかが立っているかチェック(OR演算ver)
    public bool CheckBitOR(uint targetBit) { return (bit & targetBit) != 0; }
    public static bool CheckBitOR(uint baseBit, uint targetBit) { return (baseBit & targetBit) != 0; }

    //指定したビットを立てる
    public void AddBit(uint targetBit) { bit |= (uint)targetBit; }
    public static void AddBit(uint baseBit, uint targetBit) { baseBit |= (uint)targetBit; }

    //指定したビットを折る
    public void FoldBit(uint targetBit) { bit &= (uint)~targetBit; }
    public static void FoldBit(uint baseBit, uint targetBit) { baseBit &= (uint)~targetBit; }

    //指定したビットを反転
    public void SwitchBit(uint targetBit) { bit ^= (uint)targetBit; }
    public static void SwitchBit(uint baseBit, uint targetBit) { baseBit ^= (uint)targetBit; }

    //全てのビットを立てる
    public void AddALLBit() { bit = 0xffff; }
    public static void AddALLBit(uint baseBit) { baseBit = 0xffff; }

    //全てのビットを折る
    public void FoldALLBit() { bit = 0; }
    public static void FoldALLBit(uint baseBit) { baseBit = 0; }
}
