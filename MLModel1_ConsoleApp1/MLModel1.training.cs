﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace MLModel1_ConsoleApp1
{
    public partial class MLModel1
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"S_Id", @"S_Id"),new InputOutputColumnPair(@"Sem", @"Sem"),new InputOutputColumnPair(@"IT", @"IT")})      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"S_Name", @"S_Name"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"S_Id",@"Sem",@"IT",@"S_Name"}))      
                                    .Append(mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))      
                                    .Append(mlContext.Regression.Trainers.LbfgsPoissonRegression(l1Regularization:0.203852719932089F,l2Regularization:0.259548893376871F,labelColumnName:@"AGRI",featureColumnName:@"Features"));

            return pipeline;
        }
    }
}
