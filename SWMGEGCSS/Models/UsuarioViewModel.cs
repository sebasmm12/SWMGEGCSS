﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using SWMGEGCSS_EN;
namespace SWMGEGCSS.Models
{
    public class UsuarioViewModel
    {
        public T_usuario Usuario { get; set; }

        public List<T_usuario> list_usuario { get; set; }

        public IPagedList<T_usuario> IPlist_Usuario { get; set; }

    }
}