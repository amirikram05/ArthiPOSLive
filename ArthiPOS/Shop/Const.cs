namespace ArthiPOS.shop
{
    class Const
    {
        public static int Header_Height;
        public static int Header_Width;
        public static int Dash_Menu_Left_Width;
        public static string REGKEY = "RegKey";
        public static string SECURITYKEY = "SecurityKey";



        #region Settings For User Deletion and other
        private static int _bill_Delete_After_Days = 2;
        public static int _Bill_Delete_After_Days
        {
            get
            {
                return _bill_Delete_After_Days;
            }
        }
        #endregion

    }
}
