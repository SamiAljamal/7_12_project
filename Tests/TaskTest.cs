using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ToDoList
{
  public class ToDoTest : IDisposable
  {
    public ToDoTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=todo_tests;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Task.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Task.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
       DateTime? taskDate = new DateTime(2016, 7, 12);
      Task firstTask = new Task("Mow the lawn", 1, taskDate);
      Task secondTask = new Task("Mow the lawn", 1, taskDate);

      //Assert
      Assert.Equal(firstTask, secondTask);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
       DateTime? taskDate = new DateTime(2016, 7, 12);
      Task testTask = new Task("Mow the lawn", 1, taskDate);

      //Act
      testTask.Save();
      List<Task> result = Task.GetAll();
      List<Task> testList = new List<Task>{testTask};


      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
       DateTime? taskDate = new DateTime(2016, 7, 12);
      Task testTask = new Task("Mow the lawn", 1, taskDate);
      Console.WriteLine(testTask.GetDescription());

      //Act
      testTask.Save();
      Task savedTask = Task.GetAll()[0];
      Console.WriteLine(savedTask.GetDescription());

      int result = savedTask.GetId();
      int testId = testTask.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindFindsTaskInDatabase()
    {
      //Arrange
       DateTime? taskDate = new DateTime(2016, 7, 12);
      Task testTask = new Task("Mow the lawn", 1, taskDate);
      testTask.Save();

      //Act
      Task foundTask = Task.Find(testTask.GetId());

      //Assert
      Assert.Equal(testTask, foundTask);
    }


  }
}
