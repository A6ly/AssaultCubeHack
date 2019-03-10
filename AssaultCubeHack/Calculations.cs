using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultCubeHack
{

    static class Calculations
    {

        public static float PI = (float)(Math.PI);

        public static float[] WorldToScreen(byte[] matrix, LocatableEntity entity, POINTS gameWindow, bool foot = true)
        {
            int width = gameWindow.Right - gameWindow.Left;  //게임 창의 폭을 계산
            int height = gameWindow.Bottom - gameWindow.Top; //게임 창의 너비를 계산


            
            // 매트릭스를 나중에 사용할 수 있도록 변수로 읽기 
            
            float m11 = BitConverter.ToSingle(matrix, 0), m12 = BitConverter.ToSingle(matrix, 4), m13 = BitConverter.ToSingle(matrix, 8), m14 = BitConverter.ToSingle(matrix, 12);
            float m21 = BitConverter.ToSingle(matrix, 16), m22 = BitConverter.ToSingle(matrix, 20), m23 = BitConverter.ToSingle(matrix, 24), m24 = BitConverter.ToSingle(matrix, 28);
            float m31 = BitConverter.ToSingle(matrix, 32), m32 = BitConverter.ToSingle(matrix, 36), m33 = BitConverter.ToSingle(matrix, 40), m34 = BitConverter.ToSingle(matrix, 44);
            float m41 = BitConverter.ToSingle(matrix, 48), m42 = BitConverter.ToSingle(matrix, 52), m43 = BitConverter.ToSingle(matrix, 56), m44 = BitConverter.ToSingle(matrix, 60);

            float zPos = entity is Player ? ((Player)entity).getZPos(foot) : entity.getZPos();

            //벡터에 행렬을 곱하다
            float screenX = (m11 * entity.xPos) + (m21 * entity.yPos) + (m31 * zPos) + m41;
            float screenY = (m12 * entity.xPos) + (m22 * entity.yPos) + (m32 * zPos) + m42;
            float screenW = (m14 * entity.xPos) + (m24 * entity.yPos) + (m34 * zPos) + m44;


            //카메라 위치(눈높이/화면 가운데)
            float camX = width / 2f;
            float camY = height / 2f;

            //같은 위치로 변환
            float x = camX + (camX * screenX / screenW);
            float y = camY - (camY * screenY / screenW);
            float[] screenPos = { x, y };

            //한계인 것을 확인
            if (screenW > 0.001f  //우리 뒤가 아닌
                && gameWindow.Left + x > gameWindow.Left && gameWindow.Left + x < gameWindow.Right
                && gameWindow.Top + y > gameWindow.Top && gameWindow.Top + y < gameWindow.Bottom)
            {
                return screenPos;
            }
            return null;
        }

    }
}
