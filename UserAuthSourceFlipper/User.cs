// Copyright (c) 2012 Integryst, LLC, http://www.integryst.com/
// See LICENSE.txt for licensing information

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace UserAuthSourceFlipper
{
    public class userComparer : IComparer
    {
        public userComparer() : base() { }

        int IComparer.Compare(Object x, Object y)
        {
            User left = (User)x;
            User right = (User)y;

            try
            {
                return (left.getMappingAuthName().CompareTo(right.getMappingAuthName()));
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }

    public class User
    {
        private int id;
        private String name;
        private int authSourceID;
        private String loginName;
        private String mappingAuthName;

        //private String description;
        //private String authUserName;
        //private String authUniqueName;

        public User()
        {
        }

        public int getID() { return id; }
        public void setID(int id) { this.id = id; }

        public String getName() { return name; }
        public void setName(String name) { this.name = name.ToLower(); }

        public int getAuthSourceID() { return authSourceID; }
        public void setAuthSourceID(int authSourceID) { this.authSourceID = authSourceID; }

        public String getLoginName() { return loginName; }
        public void setLoginName(String loginName) { this.loginName = loginName.ToLower(); }

        public String getMappingAuthName() { return mappingAuthName; }
        public void setMappingAuthName(String mappingAuthName) { this.mappingAuthName = mappingAuthName.ToLower(); }

    }
}
