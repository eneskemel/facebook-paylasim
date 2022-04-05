using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace facebook_paylasim
{
    class Pylsm
    {
        int id;
        String paylasim;
        String paylasanlar;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Paylasim
        {
            get
            {
                return paylasim;
            }

            set
            {
                paylasim = value;
            }
        }

        public string Paylasanlar
        {
            get
            {
                return paylasanlar;
            }

            set
            {
                paylasanlar = value;
            }
        }
    } 
}
