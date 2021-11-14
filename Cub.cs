using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Proiect
{
    class Cub
    {
        private bool visibility;
        private bool isGravityBound=true;
        private Color color1;
        private const int GRAVITY_OFFSET = 1;
        private const String FILENAME = "assets/cub.txt";

        private List<Vector3> coordsList;

        public Cub(Color col)
        {


            try
            {
                coordsList = LoadFromObjFile(FILENAME);

                if (coordsList.Count == 0)
                {
                    Console.WriteLine("Crearea obiectului a esuat: obiect negasit/coordonate lipsa!");
                    return;
                }
                visibility = true;
                color1 = col;
                Console.WriteLine("Obiect 3D încarcat - " + coordsList.Count.ToString() + " vertexuri disponibile!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: assets file <" + FILENAME + "> is missing!!!");
                
            }
        }
       
        private void ToggleVisibility()
        {
            visibility = !visibility;
        }

        public void ToggleGravity()
        {
            isGravityBound = !isGravityBound;
        }
        public void SetGravity()
        {
            isGravityBound = true;
        }
        public void UnsetGravity()
        {
            isGravityBound = false;
        }

        private List<Vector3> LoadFromObjFile(string fname)
        {
            List<Vector3> vlc3 = new List<Vector3>();


            var lines = File.ReadLines(fname);
            foreach (var line in lines)
            {
                string[] block = line.Trim().Split(' ');
                float xval = float.Parse(block[0].Trim());
                float yval = float.Parse(block[1].Trim());
                float zval = float.Parse(block[2].Trim());
                Console.WriteLine("{0},{1},{2}", block[0], block[1], block[2]);
                vlc3.Add(new Vector3((int)xval, (int)yval+60, (int)zval));

            }

            return vlc3;
        }
        public void DrawCube()
        {
            if (visibility)
            {
                GL.Color3(color1);
                GL.Begin(BeginMode.Quads);

                foreach (var vert in coordsList)
                {


                    GL.Vertex3(vert);
                }
                GL.End();
            }
        }
        public void UpdatePosition() 
        {
            if (visibility && isGravityBound && !GroundColectionDetected()) 
            {
                for(int i=0; i < coordsList.Count; i++)
                {

                    coordsList[i] = new Vector3(coordsList[i].X, coordsList[i].Y -GRAVITY_OFFSET,coordsList[i].Z);

                    
                }
                
               
            }
        }
        public bool GroundColectionDetected() 
        {
            foreach (Vector3 v in coordsList) 
            {
                if (v.Y <= 0) 
                {
                    return true;
                }
            }
            return false;
        }
       
      
       
    }
}


