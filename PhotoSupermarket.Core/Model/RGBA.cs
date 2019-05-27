namespace PhotoSupermarket.Core.Model
{
    public class RGBA : RGB
    {
        public byte A { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            RGBA rgbaObj = (RGBA)obj;
            return R == rgbaObj.R && G == rgbaObj.G && B == rgbaObj.B && rgbaObj.A == rgbaObj.A;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return R ^ G ^ B ^ A;
        }
    }
}