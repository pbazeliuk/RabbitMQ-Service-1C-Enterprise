using System;
using System.Collections.Generic;
using System.Reflection;
using RabbitMQ.Client;

namespace ProxyService.Services
{
    public class RabbitMQConnectionFactory : ConnectionFactory
    {
        public RabbitMQConnectionFactory(Dictionary<string, String> fields)
            : base()
        {
            Type type = base.GetType();
            FieldInfo[] fieldsInfo = type.GetFields();
            foreach (var field in fields)
            {
                FieldInfo result = Array.Find<FieldInfo>(fieldsInfo,
                                        fi => fi.Name == field.Key);
                if (result != null)
                {
                    string sType = result.FieldType.FullName;
                    result.SetValue(this, Convert.ChangeType(field.Value,
                                            Type.GetType(sType)));
                }
            }
        }
    }
}