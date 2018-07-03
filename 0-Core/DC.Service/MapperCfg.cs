using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DC.Data.Common.DataManage;
using DC.Domain.DataManage;

namespace DC.Service
{
    public static class MapperCfg
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TableInfo, TableInfoDto>();
            });
        }
    }
}
