﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace BF2Statistics.Database.QueryBuilder
{
    class InsertQueryBuilder
    {
        #region Properties

        /// <summary>
        /// The table name to query
        /// </summary>
        public string Table;

        /// <summary>
        /// A list of FieldValuePairs
        /// </summary>
        protected Dictionary<string, object> Fields = new Dictionary<string, object>();

        /// <summary>
        /// The database driver, if using the "BuildCommand" method
        /// </summary>
        protected DatabaseDriver Driver;

        #endregion Properties

        #region Constructors

        public InsertQueryBuilder() { }

        public InsertQueryBuilder(DatabaseDriver Driver)
        {
            this.Driver = Driver;
        }

        public InsertQueryBuilder(string Table, DatabaseDriver Driver)
        {
            this.Table = Table;
            this.Driver = Driver;
        }

        #endregion Constructors

        public void SetField(string Field, object Value)
        {
            if (Fields.ContainsKey(Field))
                Fields[Field] = Value;
            else
                Fields.Add(Field, Value);
        }

        #region Set Methods

        /// <summary>
        /// Sets the table name to inesrt into
        /// </summary>
        /// <param name="Table">The name of the table to insert into</param>
        public void SetTable(string Table)
        {
            this.Table = Table;
        }

        /// <summary>
        /// Sets the database driver
        /// </summary>
        /// <param name="Driver"></param>
        public void SetDbDriver(DatabaseDriver Driver)
        {
            this.Driver = Driver;
        }

        #endregion Set Methods

        #region Query

        /// <summary>
        /// Builds the query string with the current SQL Statement, and returns
        /// the querystring.
        /// </summary>
        /// <returns></returns>
        public string BuildQuery()
        {
            return BuildQuery(false) as String;
        }

        /// <summary>
        /// Builds the query string with the current SQL Statement, and
        /// returns the DbCommand to be executed
        /// </summary>
        /// <returns></returns>
        public DbCommand BuildCommand()
        {
            return BuildQuery(true) as DbCommand;
        }

        /// <summary>
        /// Builds the query string or DbCommand
        /// </summary>
        /// <param name="BuildCommand"></param>
        /// <returns></returns>
        protected object BuildQuery(bool BuildCommand)
        {
            // Make sure we have a valid DB driver
            if (BuildCommand && Driver == null)
                throw new Exception("Cannot build a command when the Db Drvier hasn't been specified. Call SetDbDriver first.");

            // Make sure we have a table name
            if (String.IsNullOrWhiteSpace(Table))
                throw new Exception("Table to insert into was not set.");

            // Make sure we have at least 1 field to update
            if (Fields.Count == 0)
                throw new Exception("No fields to insert");

            // Create Command
            DbCommand Command = (BuildCommand) ? Driver.CreateCommand(null) : null;

            // Start Query
            StringBuilder Query = new StringBuilder("INSERT INTO " + Table + " (");
            StringBuilder Values = new StringBuilder();
            bool First = true;

            // Add fields and values
            foreach (KeyValuePair<string, object> Item in Fields)
            {
                // Append comma
                if (!First)
                {
                    Query.Append(", ");
                    Values.Append(", ");
                }
                else 
                    First = false;

                // If using a command, Convert values to Parameters
                if (BuildCommand && Item.Value != null && Item.Value != DBNull.Value && !(Item.Value is SqlLiteral))
                {
                    // Create param for value
                    DbParameter Param = Command.CreateParameter();
                    Param.ParameterName = "@P" + Command.Parameters.Count;
                    Param.Value = Item.Value;

                    // Add Params to command
                    Command.Parameters.Add(Param);

                    // Append query's
                    Query.Append(Item.Key);
                    Values.Append(Param.ParameterName);
                }
                else
                {
                    Query.Append(Item.Key);
                    Values.Append(WhereStatement.FormatSQLValue(Item.Value));
                }
            }

            // Finish the query string, and return the proper object
            Query.Append(") VALUES (" + Values.ToString() + ")");

            // Set the command text
            if (BuildCommand) Command.CommandText = Query.ToString();
            return (BuildCommand) ? Command as object : Query.ToString();
        }

        /// <summary>
        /// Executes the command against the database. The database driver must be set!
        /// </summary>
        /// <returns></returns>
        public int Execute()
        {
            Driver.NumQueries++;
            using (DbCommand Command = BuildCommand())
                return Command.ExecuteNonQuery();
        }

        #endregion Query
    }
}
