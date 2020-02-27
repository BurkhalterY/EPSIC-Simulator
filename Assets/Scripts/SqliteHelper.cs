﻿using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;
using System;

public class SqliteHelper
{
    private const string database_name = "epsic_simulator";

    private string db_connection_string;
    private IDbConnection db_connection;

    public SqliteHelper()
    {
        db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
        db_connection = new SqliteConnection(db_connection_string);
        db_connection.Open();
    }

    ~SqliteHelper()
    {
        close();
    }

    public void createDatabase()
    {
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS questions(id INTEGER PRIMARY KEY AUTOINCREMENT, question TEXT, image_path VARCHAR, date_insert DATETIME);";
        dbcmd.ExecuteReader();
        dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS answers(id INTEGER PRIMARY KEY AUTOINCREMENT, answer TEXT, image_path VARCHAR, source_answer TEXT, points INTEGER, date_insert DATETIME);";
        dbcmd.ExecuteReader();
        dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS questions_answers(id INTEGER PRIMARY KEY AUTOINCREMENT, fk_question INTEGER, fk_answer INTEGER, date_insert DATETIME);";
        dbcmd.ExecuteReader();
        dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS categories(id INTEGER PRIMARY KEY AUTOINCREMENT, category_folder VARCHAR, category_name VARCHAR, date_insert DATETIME);";
        dbcmd.ExecuteReader();
        dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS questions_categories(id INTEGER PRIMARY KEY AUTOINCREMENT, fk_question INTEGER, fk_category INTEGER, date_insert DATETIME);";
        dbcmd.ExecuteReader();
        dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS sub_categories(id INTEGER PRIMARY KEY AUTOINCREMENT, sub_category_name VARCHAR, date_insert DATETIME);";
        dbcmd.ExecuteReader();
        dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS questions_sub_categories(id INTEGER PRIMARY KEY AUTOINCREMENT, fk_question INTEGER, fk_sub_category INTEGER, date_insert DATETIME);";
        dbcmd.ExecuteReader();
    }

    public IDataReader getDataById(string table_name, int id)
    {
        return getDataByString(table_name, "id", id.ToString());
    }

    public IDataReader getDataById(string table_name, string id)
    {
        return getDataByString(table_name, "id", id);
    }

    public IDataReader getDataByString(string table_name, string key, string value)
    {
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText = "SELECT * FROM " + table_name + " WHERE " + key + " = '" + value + "'";
        return dbcmd.ExecuteReader();
    }

    public IDataReader insert(string table_name, string[] data)
    {
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText = "INSERT INTO " + table_name + " VALUES (";
        string values = "";
        foreach (string str in data)
        {
            if (values != "")
            {
                values += ", ";
            }
            values += "'" + str + "'";
        }
        dbcmd.CommandText += values + ");";
        return dbcmd.ExecuteReader();
    }

    public void deleteDataById(string table_name, string id)
    {
        deleteDataByString(table_name, "id", id);
    }

    public void deleteDataByString(string table_name, string key, string value)
    {
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText = "DELETE FROM " + table_name + " WHERE " + key + " = '" + value + "'";
        dbcmd.ExecuteNonQuery();
    }

    public IDbCommand getDbCommand()
    {
        return db_connection.CreateCommand();
    }

    public IDataReader getAllData(string table_name)
    {
        IDbCommand dbcmd = db_connection.CreateCommand();
        dbcmd.CommandText = "SELECT * FROM " + table_name;
        return dbcmd.ExecuteReader();
    }

    public void deleteAllData(string table_name)
    {
        IDbCommand dbcmd = db_connection.CreateCommand();
        dbcmd.CommandText = "DROP TABLE IF EXISTS " + table_name;
        dbcmd.ExecuteNonQuery();
    }

    public int count(string table_name, string key, string value)
    {
        IDbCommand dbcmd = db_connection.CreateCommand();
        dbcmd.CommandText = "SELECT COUNT(id) FROM " + table_name + " WHERE " + key + " = '" + value + "'"; ;
        return Convert.ToInt32(dbcmd.ExecuteScalar());
    }

    public void close()
    {
        db_connection.Close();
    }
}