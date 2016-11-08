using System;

namespace Minotaur
{
    static class Misc
    {
        public static void ArrayAppend<T>(ref T[] array, params T[] obj)
        {
            int formerLength = array.Length;
            Array.Resize(ref array, formerLength + obj.Length);

            for(int i = 0; i < obj.Length; i++)
            {
                array[formerLength + i] = obj[i];
            }
        }

        public static void ArrayRemove<T>(ref T[] array, params int[] id)
        {
            if (array.Length <= 0 || id.Length <= 0)
                return;

            Array.Sort(id);

            int currentIndex = 0;
            int moveOffset = 0;

            for(int i = id[currentIndex]; i < array.Length; i++)
            {
                if(i == id[currentIndex])
                {
                    moveOffset++;

                    if (currentIndex + 1 < id.Length)
                        currentIndex++;
                }
                else
                {
                    array[i - moveOffset] = array[i];
                }
            }

            Array.Resize(ref array, array.Length - moveOffset);
        }

        public static void ArrayRemoveLast<T>(ref T[] array)
        {
            if (array.Length > 0)
                Array.Resize(ref array, array.Length - 1);
        }
    }
}
