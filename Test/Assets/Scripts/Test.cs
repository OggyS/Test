using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        private static Vector3[][] Grid { get; set; }
        private static float heightOfGrid { get; set; }
        public Vector3()
        {
            X = 0f;
            Y = 0f;
            Z = 0f;
        }
        public Vector3(float _x, float _y, float _z)
        {
            X = _x;
            Y = _y;
            Z = _z;
        }
        public Vector3(int _x, int _y, int _z)
        {
            X = (float)_x;
            Y = (float)_y;
            Z = (float)_z;
        }
        public Vector3(double _x, double _y, double _z)
        {
            X = (float)_x;
            Y = (float)_y;
            Z = (float)_z;
        }
        public static implicit operator UnityEngine.Vector3(Vector3 v)
        {
            return new UnityEngine.Vector3(v.X,v.Y,v.Z);
        }
        public static implicit operator Vector3(UnityEngine.Vector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }
        public float DotProduct(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z *v2.Z;
        }
        public Vector3 VectorProduct(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.Y*v2.Z-v1.Z*v2.Y, v1.Z*v2.X-v1.X*v2.Z, v1.X*v2.Y-v1.Y*v2.X);
        }
        public void Normalize()
        {
            float _magnitude = Magnitude();
            if (_magnitude > 0)
            {
                X = X / _magnitude;
                Y = Y / _magnitude;
                Z = Z / _magnitude;
            } 
            else
            {
                X = 0;
                Y = 0;
                Z = 0;
            }
            
        }
        public float Magnitude()
        {
            return Mathf.Sqrt(X * X + Y * Y + Z * Z); 
        }
        public static Vector3[][] CreateGrid(int width, int length, int pointHeight, int deltaX, int deltaY)
        {
            Grid = new Vector3[length][];
            for (int i = 0; i < length; i++)
            {
                Grid[i] = new Vector3[width];
                for (int j = 0; j < width; j++)
                {
                    Grid[i][j] = new Vector3(deltaX*j, pointHeight,deltaY*i);
                   
                }
            }
            heightOfGrid = pointHeight;
            return Grid;
        }
        public Vector3[][] StirUpGrid(float time, float wavelength)
        {
            
            for(int i = 0; i < Grid.Length; i++)//duzina grida
            {
                for(int j = 0; j <Grid[i].Length; j++)
                {
                    Grid[i][j] = new Vector3(Grid[i][j].X,heightOfGrid+ Mathf.Sin( DistOnXZ(Grid[i][j].X, Grid[i][j].Y, Grid[i][j].Z).Magnitude()/wavelength - time ), Grid[i][j].Z);
                   
                }
            }
            //Debug.Log(Grid.Length);
            return Grid;
        }
        private Vector3 DistOnXZ(float x, float y, float z)
        {
            Vector3 _centreOfGrid = Grid[Grid.Length / 2][Grid[0].Length / 2];
            Vector3 v3 = new Vector3(x - _centreOfGrid.X,0, z - _centreOfGrid.Z);
            return v3;
        }

       
    }
    public class Matrix4x4
    {
        private float M00 { get; set; }
        private float M10 { get; set; }
        private float M20 { get; set; }
        private float M30 { get; set; }

        private float M01 { get; set; }
        private float M11 { get; set; }
        private float M21 { get; set; }
        private float M31 { get; set; }

        private float M02 { get; set; }
        private float M12 { get; set; }
        private float M22 { get; set; }
        private float M32 { get; set; }

        private float M03 { get; set; }
        private float M13 { get; set; }
        private float M23 { get; set; }
        private float M33 { get; set; }

        public Matrix4x4(float _M00, float _M01, float _M02, float _M03,
                         float _M10, float _M11, float _M12, float _M13,
                         float _M20, float _M21, float _M22, float _M23,
                         float _M30, float _M31, float _M32, float _M33)
        {
            M00 = _M00; M01 = _M01; M02 = _M02; M03 = _M03;
            M10 = _M10; M11 = _M11; M12 = _M12; M13 = _M13;
            M20 = _M20; M21 = _M21; M22 = _M22; M23 = _M23;
            M30 = _M30; M31 = _M31; M32 = _M32; M33 = _M33;
        }

        public Matrix4x4 TransposedMatrix(Matrix4x4 matrix4X4)
        {
            Matrix4x4 m4x4 = new Matrix4x4(matrix4X4.M00, matrix4X4.M10, matrix4X4.M20, matrix4X4.M30,
                                           matrix4X4.M01, matrix4X4.M11, matrix4X4.M21, matrix4X4.M31,
                                           matrix4X4.M02, matrix4X4.M12, matrix4X4.M22, matrix4X4.M32,
                                           matrix4X4.M03, matrix4X4.M13, matrix4X4.M23, matrix4X4.M33);
            return m4x4;
        }

        public Vector3 MatrixScale(Matrix4x4 matrix4X4)
        {
            Vector3 v3 = new Vector3(matrix4X4.M00, matrix4X4.M11, matrix4X4.M22);
            return v3;
        }
        public Vector3 MatrixPosition(Matrix4x4 matrix4X4)
        {
            Vector3 v3 = new Vector3(matrix4X4.M03, matrix4X4.M13, matrix4X4.M23);
            return v3;
        }
        //https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles
        //http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
        public Vector3 MatrixRoation(Matrix4x4 matrix4X4)
        {
            float w,x, y, z;
            w=Mathf.Sqrt(Mathf.Max(0, 1 + matrix4X4.M00 + matrix4X4.M11 + matrix4X4.M22)) / 2;
            x = Mathf.Sqrt(Mathf.Max(0, 1 + matrix4X4.M00 - matrix4X4.M11 - matrix4X4.M22)) / 2;
            y = Mathf.Sqrt(Mathf.Max(0, 1 - matrix4X4.M00 + matrix4X4.M11 - matrix4X4.M22)) / 2;
            z = Mathf.Sqrt(Mathf.Max(0, 1 - matrix4X4.M00 - matrix4X4.M11 + matrix4X4.M22)) / 2;
            x *= Mathf.Sign(x * (matrix4X4.M21 - matrix4X4.M12));
            y *= Mathf.Sign(y * (matrix4X4.M02 - matrix4X4.M20));
            z *= Mathf.Sign(z * (matrix4X4.M10 - matrix4X4.M01));
            float roll, pitch, yaw;
            
            double sinr_cosp = 2 * (w*x+y*z);
            double cosr_cosp = 1 - 2 * (x * x + y * y);
            roll = (float)Math.Atan2(sinr_cosp, cosr_cosp);

            double sinp = 2 * (w * y - z * x);
            if (Math.Abs(sinp) >= 1)
            {
                if (sinp >= 0)//copy sign
                {
                    pitch = (float)Math.PI / 2 * 1;
                }
                else
                {
                    pitch = (float)Math.PI / 2 * -1;
                }
            }
            else
            {
                pitch = (float)Math.Asin(sinp);
            }

            double siny_cosp = 2 * (w * z + x * y);
            double cosy_cosp = 1 - 2 * (y * y + z * z);
            yaw = (float)Math.Atan2(siny_cosp, cosy_cosp);
            return new Vector3(roll,pitch,yaw);
        }


    }
    public class Transform
    {//-
        private Vector3 localPosition;
        private Vector3 localRotation;
        private Vector3 localScale;
        private Transform parent;
        private Transform[] children;

        public Vector3 LocalPosition
        {
            get
            {
                return localPosition;
            }
            set
            {
                localPosition = value;
            }
        }
        public Vector3 LocalRotation
        {
            get
            {
                return localRotation;
            }
            set
            {
                localRotation = value;
            }
        }
        public Vector3 LocalScale
        {
            get
            {
                return localScale;
            }
            set
            {
                localScale = value;
            }
        }
        public Transform Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }
        public Transform[] Children
        {
            get
            {
                return children;
            }
            set
            {
                children = value;
            }
        }
        public Vector3 Position
        {
            get
            {
                if (parent == null)
                {
                    return localPosition;
                }
                else
                {
                    return new Vector3(parent.localPosition.X + localPosition.X, parent.localPosition.Y + localPosition.Y, parent.localPosition.Z + localPosition.Z);
                }
            }
            set
            {
                if (parent == null)
                {
                     localPosition=value;
                }
                else
                {
                    localPosition= new Vector3(parent.localPosition.X + localPosition.X, parent.localPosition.Y + localPosition.Y, parent.localPosition.Z + localPosition.Z);
                }
            }
        }
        public Vector3 Scale
        {
            get
            {
                if (parent == null)
                {
                    return localScale;
                }
                else
                {
                    return new Vector3(localScale.X*parent.localScale.X, localScale.Y*parent.localScale.Y, localScale.Z*parent.localScale.Z);
                }
            }
            set
            {
                if (parent == null)
                {
                    localScale = value;
                }
                else
                {
                    localPosition= new Vector3(localScale.X * parent.localScale.X, localScale.Y * parent.localScale.Y, localScale.Z * parent.localScale.Z);
                }
            }
        }
        //public Vector3 Roation
         public Vector3 GetWorldPosition()
        {
            Vector3 v3;
            Transform trans=new Transform();
            if (trans.Parent==null)
            {
                return trans.LocalPosition;
            }
            else
            {
                do
                {
                    v3 = trans.Position;
                    trans = trans.Parent;
                } while (trans.Parent!=null);
                return v3;
            }
        }
        //public Vector3 GetWorldRotation();
       // public Matrix4x4 GetObjectToWorldMatrix()
        
    }
    public class TransformCollection
    {

        

    }
}