﻿using MyBlog.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class BlogNewsRepository:BaseRepository<Model.BlogNews>,IBlogNewsRepository
    {
    }
}
