﻿using Coderr.Server.Domain.Modules.Similarities;
using Griffin.Data.Mapper;

namespace Coderr.Server.PostgreSQL.Modules.Similarities.Mappers
{
    public class SimilarityValueMapper : CrudEntityMapper<SimilarityValue>
    {
        public SimilarityValueMapper()
            : base("SimilarityValues")
        {
            //Property(x => x.Id).PrimaryKey(true);
        }
    }
}