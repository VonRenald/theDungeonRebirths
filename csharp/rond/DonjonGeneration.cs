using System;
using System.Drawing; // dotnet add package System.Drawing.Common 

namespace GenerationDonjon2
{
    class Vecteur
    {
        public float x,y;
        public Vecteur toVec(Point p1, Point p2)
        {
            Vecteur vec = new Vecteur(((p1.x>p2.x)? p1.x-p2.x: p2.x-p1.x),((p1.y>p2.y)? p1.y-p2.y: p2.y-p1.y));
            return vec;
        }
        public Vecteur(float x=0, float y=0)
        {
            init(x,y);
        }
        public Vecteur(Vecteur vec)
        {
            init(vec.x,vec.y);
        }
        public Vecteur(Point p1, Point p2)
        {
            init(x,y);
        }
        private void init(float x, float y)
        {
            this.x=x;
            this.y=y;
        }
    }
    class Point
    {
        public int x,y;
        public float getDist(Point b)
        {
            return getDist(b,this);
        }
        public float getDist(Point b, Point a)
        {   
            Point min = b-a;
            min *= min;
            return (float)Math.Sqrt(min.x+min.y);
        }
        public Point(Point p)
        {
            init(p.x,p.y);
        }
        public Point(int x=0,int y=0)
        {
            init(x,y);
        }
        private void init(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
        public static Point operator+ (Point p1,Point p2)
        {
            return (new Point(p1.x+p2.x,p1.y+p2.y));
        }
        public static Point operator- (Point p1,Point p2)
        {
            return (new Point(p1.x-p2.x,p1.y-p2.y));
        }
        public static Point operator* (Point p1,Point p2)
        {
            return (new Point(p1.x*p2.x,p1.y*p2.y));
        }
        public static Point operator/ (Point p1,Point p2)
        {
            return (new Point(p1.x/p2.x,p1.y/p2.y));
        }
        public static bool operator== (Point p1,Point p2)
        {
            return (p1.x==p2.x && p1.y==p2.y);
        }
        public static bool operator!= (Point p1,Point p2)
        {
            return (p1.x!=p2.x || p1.y!=p2.y);
        }
        public override string ToString()
        {
            return String.Format("({0}, {1})", x, y);
        }
    }
    class Cercle
    {
        public Point center = new Point();
        public Point old = new Point();
        public float radius = 0;
        public List<Cercle> link = new List<Cercle>();
        public Cercle(int x=0,int y=0,float r=1)
        {
            init(x,y,r);
        }
        public Cercle(Point p,float r=1)
        {
            init(p.x,p.y,r);
        }
        private void init(int x, int y, float r)
        {
            this.center = new Point(x,y);
            this.radius = r;
        }
        public static bool operator== (Cercle c1,Cercle c2)
        {
            return (c1.center == c2.center && c1.radius == c2.radius);
        }
        public static bool operator!= (Cercle c1,Cercle c2)
        {
            return (c1.center != c2.center || c1.radius != c2.radius);
        }
        public override string ToString()
        {
            return String.Format("({0}-r {1})", center, radius);
        }
    }
    class Piece
    {
        public float angle(Point p1, Point p2, Point p3){
            double a = Math.Pow(p1.x - p2.x,2) + Math.Pow(p1.y-p2.y,2);
            double b = Math.Pow(p1.x - p3.x,2) + Math.Pow(p1.y-p3.y,2);
            double c = Math.Pow(p2.x - p3.x,2) + Math.Pow(p2.y-p3.y,2);

            double angle = Math.Acos((a+b-c) / Math.Sqrt(4*a*b));

            return (float)(angle*(180.0/Math.PI));
        }
        public bool inCerclesExept(Point P, params Cercle[] Cs)
        {
            foreach(Cercle c in cercles)
            {
                if(P.getDist(c.center)<=c.radius && !Cs.Contains(c)) return true;
            }
            return false;
        }
        public bool inCercles(Point P, params Cercle[] Cs)
        {
            if(Cs.Count() == 0){
                foreach(Cercle c in cercles)
                {
                    if(P.getDist(c.center)<=c.radius) return true;
                }
                return false;
            }
            foreach(Cercle c in Cs)
            {
                if(P.getDist(c.center)<=c.radius) return true;
            }
            return false;
        }


        //VAR
        public List<Cercle> cercles = new List<Cercle>();
        public List<Point> inPoints = new List<Point>();
        public List<Point> outPoints = new List<Point>();
        public List<Point> wall = new List<Point>();
        public List<Point> wallreduce = new List<Point>();

        public static List<Point> ConvexHull(List<Point> points)
        {
            if (points.Count <= 1)
                return points;

            var hull = new List<Point>();
            
            // get leftmost point
            var hullPoint = points.Where(p => p.x == points.Min(min => min.x)).First();
            hull.Add(hullPoint);
            var endPoint = new Point();
            
            do
            {
                endPoint = points[0];
                
                for (int i = 1; i < points.Count; i++)
                {
                    if ((hullPoint == endPoint) 
                        || (Orientation(hullPoint, points[i], endPoint) == 2))
                    {
                        endPoint = points[i];
                    }
                }
                
                hullPoint = endPoint;
                
                if (endPoint != hull[0])
                {
                    hull.Add(endPoint);
                }
                
            } while (endPoint != hull[0]);
            
            return hull;
        }
        
        public static int Orientation(Point p, Point q, Point r)
        {
            float val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);
    
            if (val == 0) return 0;  // collinear
            return (val > 0) ? 1: 2; // clock or counterclock wise
        }
        public static Tuple<double, double, double> FindLineCoefficients(Point p1, Point p2)
        {
            double a = p2.y - p1.y;
            double b = p1.x - p2.x;
            double c = a * p1.x + b * p1.y;
            
            return new Tuple<double, double, double>(a, b, -c);
        }
        public static double DistanceFromPointToLine(double a, double b, double c, Point p)
        {
            double numerator = Math.Abs(a*p.x + b*p.y + c);
            double denominator = Math.Sqrt(a*a + b*b);
            
            return numerator / denominator;
        }

        public List<Point> reduceWall(List<Point> points, List<Point> wall_)
        {
            // List<Point> alone = new List<Point>();
            List<Point> wallR = new List<Point>();
            foreach(Point p in wall_){
                wallR.Add(p);
            }
            foreach(Point p in points){
                if(!wallR.Contains(p)){
                    // alone.Add(p);
                    float m = 9999;
                    int e = -1;
                    for(int i=0; i<wallR.Count; i++){
                        Tuple<double,double,double> fonc;
                        if (i!=wallR.Count-1) fonc = FindLineCoefficients(wallR[i],wallR[i+1]);
                        else fonc = FindLineCoefficients(wallR[i],wallR[0]);
                        
                        float dist = (float)DistanceFromPointToLine(fonc.Item1,fonc.Item2,fonc.Item3,p);
                        if(dist < m) {m = dist;e=i+1;}
                    }
                    wallR.Insert(e,p);
                }
                    
            }
            return wallR;
        }


        public Piece(int nbCercles=3, int StartX=0, int StartY=0, int angleBan = 120)
        {
            random rand = new random();

            List<Point> toDo = new List<Point>{new Point(StartX+rand.randint(-20,20),StartY+rand.randint(-20,20)),
                new Point(StartX,StartY)};
            while( cercles.Count< nbCercles){
                Point oldP = toDo[0];toDo.RemoveAt(0);
                Point startP = toDo[0];toDo.RemoveAt(0);
            //   reloop:
                Point drift,P;
                float r=0,agl;
                bool stop = false;
                do{
                    drift = new Point(rand.randint(-20,20),rand.randint(-20,20));
                    P = startP + drift;
                    r = startP.getDist(P);
                    agl = angle(startP,P,oldP);
                    stop = r >=5 && agl >= angleBan && agl < 360-angleBan;
                }while(!stop);
                Cercle cercle = new Cercle(P,r);cercle.old = startP;
                cercles.Add(cercle);
                toDo.Add(startP);toDo.Add(P);
                if(rand.randint(0,9) == 0) {
                    Console.WriteLine("reloop");
                    // goto reloop;
                    drift.x = -drift.x;
                    P = startP + drift;
                    r = startP.getDist(P);
                    cercle = new Cercle(P,r);cercle.old = startP;
                    cercles.Add(cercle);
                    toDo.Add(startP);toDo.Add(P);
                }

            }

            float[] XdriftNorm = {  1.0f,0.5f,-0.5f,-1.0f,-0.5f,0.5f}; 
            float[] YdriftNorm = {  0.0f,(float)Math.Sqrt(3)/2.0f, (float)Math.Sqrt(3)/2.0f,
                                    0.0f,-(float)Math.Sqrt(3)/2.0f,-(float)Math.Sqrt(3)/2.0f}; 
            // allPoint
            foreach(Cercle cercle in cercles){
                for(int i=0;i<6;i++)
                {
                    int x = (XdriftNorm[i]>=0)? (int)Math.Floor(XdriftNorm[i]*cercle.radius)+cercle.center.x: (int)Math.Ceiling(XdriftNorm[i]*cercle.radius)+cercle.center.x;
                    int y = (YdriftNorm[i]>=0)? (int)Math.Floor(YdriftNorm[i]*cercle.radius)+cercle.center.y: (int)Math.Ceiling(YdriftNorm[i]*cercle.radius)+cercle.center.y;
                    Point p = new Point(x,y);
                    if(inCerclesExept(p,cercle))
                        inPoints.Add(p);
                    else
                        outPoints.Add(p);
                }
            }
            
            wall = ConvexHull(outPoints);
            wallreduce = reduceWall(outPoints,wall);
        }

        public void Draw(String str)
        {
            Bitmap bmp = new Bitmap(200,200);
            Graphics g = Graphics.FromImage(bmp);
            Pen bluePen = new Pen(Color.Blue, 1);
            Pen redPen = new Pen(Color.Red, 1);

            Point startP = cercles[0].center;

            Point drift = new Point(0,0);
            foreach(Cercle c in cercles)
            {
                drift+=c.center;
            }
            drift.x /=cercles.Count;drift.y /=cercles.Count;
            Console.WriteLine("{0} {1}",drift, cercles[0].center);
            drift.x =cercles[0].center.x-drift.x; drift.y =cercles[0].center.y-drift.y;
            Console.WriteLine("{0}",drift);
            // foreach(Cercle c in cercles)
            // {
                
            //     g.DrawEllipse(bluePen, c.center.x-c.radius+drift.x, c.center.y-c.radius+drift.y, 2*c.radius, 2*c.radius);
            //     // g.DrawLine(redPen,startP.x,startP.y,c.center.x,c.center.y);
            //     // bmp.SetPixel(startP.x,startP.y,Color.Red);
            //     // bmp.SetPixel(c.center.x,c.center.y,Color.Green);
                
            // }
            // foreach(Cercle c in cercles)
            // {
            //     g.DrawLine(redPen,c.old.x+drift.x,c.old.y+drift.y,c.center.x+drift.x,c.center.y+drift.y);
            //     // startP = c.center;
            // }
            // foreach(Point p in outPoints){
            //     if(p.x>0 && p.y>0 && p.x <200 && p.y <200)
            //         bmp.SetPixel(p.x+drift.x,p.y+drift.y,Color.Red);
            // }
            // foreach(Point p in inPoints){
            //     if(p.x+drift.x>0 && p.y+drift.y>0 && p.x+drift.x <200 && p.y+drift.y <200)
            //         bmp.SetPixel(p.x+drift.x,p.y+drift.y,Color.Green);
            // }

            for(int i=0; i<wall.Count-1; i++){
                g.DrawLine(redPen, wall[i].x+drift.x, wall[i].y+drift.y, wall[i+1].x+drift.x, wall[i+1].y+drift.y);
            }g.DrawLine(redPen, wall[wall.Count-1].x+drift.x, wall[wall.Count-1].y+drift.y, wall[0].x+drift.x, wall[0].y+drift.y);
            
            // for(int i=0; i<wallreduce.Count-1; i++){
            //     g.DrawLine(bluePen, wallreduce[i].x+drift.x, wallreduce[i].y+drift.y, wallreduce[i+1].x+drift.x, wallreduce[i+1].y+drift.y);
            // }
            // g.DrawLine(bluePen, wallreduce[wallreduce.Count-1].x+drift.x, wallreduce[wallreduce.Count-1].y+drift.y, wallreduce[0].x+drift.x, wallreduce[0].y+drift.y);
            

            bmp.Save(str, System.Drawing.Imaging.ImageFormat.Png);
            g.Dispose();
            bmp.Dispose();
        }

    }
    class Donjon
    {
        public Donjon()
        {

        }
    }
}