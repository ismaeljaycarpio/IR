﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IR
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="dbIR")]
	public partial class IRContextDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCrisisCode(CrisisCode instance);
    partial void UpdateCrisisCode(CrisisCode instance);
    partial void DeleteCrisisCode(CrisisCode instance);
    partial void InsertEvidencePhoto(EvidencePhoto instance);
    partial void UpdateEvidencePhoto(EvidencePhoto instance);
    partial void DeleteEvidencePhoto(EvidencePhoto instance);
    partial void InsertSiteStatus(SiteStatus instance);
    partial void UpdateSiteStatus(SiteStatus instance);
    partial void DeleteSiteStatus(SiteStatus instance);
    partial void InsertDepartmentsInvolved(DepartmentsInvolved instance);
    partial void UpdateDepartmentsInvolved(DepartmentsInvolved instance);
    partial void DeleteDepartmentsInvolved(DepartmentsInvolved instance);
    partial void InsertIRTransaction(IRTransaction instance);
    partial void UpdateIRTransaction(IRTransaction instance);
    partial void DeleteIRTransaction(IRTransaction instance);
    #endregion
		
		public IRContextDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["dbIR"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public IRContextDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public IRContextDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public IRContextDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public IRContextDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CrisisCode> CrisisCodes
		{
			get
			{
				return this.GetTable<CrisisCode>();
			}
		}
		
		public System.Data.Linq.Table<EvidencePhoto> EvidencePhotos
		{
			get
			{
				return this.GetTable<EvidencePhoto>();
			}
		}
		
		public System.Data.Linq.Table<SiteStatus> SiteStatus
		{
			get
			{
				return this.GetTable<SiteStatus>();
			}
		}
		
		public System.Data.Linq.Table<DepartmentsInvolved> DepartmentsInvolveds
		{
			get
			{
				return this.GetTable<DepartmentsInvolved>();
			}
		}
		
		public System.Data.Linq.Table<IRTransaction> IRTransactions
		{
			get
			{
				return this.GetTable<IRTransaction>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CrisisCode")]
	public partial class CrisisCode : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Code;
		
		private string _Name;
		
		private EntitySet<IRTransaction> _IRTransactions;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCodeChanging(string value);
    partial void OnCodeChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public CrisisCode()
		{
			this._IRTransactions = new EntitySet<IRTransaction>(new Action<IRTransaction>(this.attach_IRTransactions), new Action<IRTransaction>(this.detach_IRTransactions));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", DbType="VarChar(50)")]
		public string Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this.OnCodeChanging(value);
					this.SendPropertyChanging();
					this._Code = value;
					this.SendPropertyChanged("Code");
					this.OnCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(MAX)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CrisisCode_IRTransaction", Storage="_IRTransactions", ThisKey="Id", OtherKey="CrisisId")]
		public EntitySet<IRTransaction> IRTransactions
		{
			get
			{
				return this._IRTransactions;
			}
			set
			{
				this._IRTransactions.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_IRTransactions(IRTransaction entity)
		{
			this.SendPropertyChanging();
			entity.CrisisCode = this;
		}
		
		private void detach_IRTransactions(IRTransaction entity)
		{
			this.SendPropertyChanging();
			entity.CrisisCode = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.EvidencePhoto")]
	public partial class EvidencePhoto : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private System.Nullable<int> _IrId;
		
		private string _ImagePath;
		
		private EntityRef<IRTransaction> _IRTransaction;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnIrIdChanging(System.Nullable<int> value);
    partial void OnIrIdChanged();
    partial void OnImagePathChanging(string value);
    partial void OnImagePathChanged();
    #endregion
		
		public EvidencePhoto()
		{
			this._IRTransaction = default(EntityRef<IRTransaction>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IrId", DbType="Int")]
		public System.Nullable<int> IrId
		{
			get
			{
				return this._IrId;
			}
			set
			{
				if ((this._IrId != value))
				{
					if (this._IRTransaction.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIrIdChanging(value);
					this.SendPropertyChanging();
					this._IrId = value;
					this.SendPropertyChanged("IrId");
					this.OnIrIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ImagePath", DbType="VarChar(MAX)")]
		public string ImagePath
		{
			get
			{
				return this._ImagePath;
			}
			set
			{
				if ((this._ImagePath != value))
				{
					this.OnImagePathChanging(value);
					this.SendPropertyChanging();
					this._ImagePath = value;
					this.SendPropertyChanged("ImagePath");
					this.OnImagePathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="IRTransaction_EvidencePhoto", Storage="_IRTransaction", ThisKey="IrId", OtherKey="Id", IsForeignKey=true, DeleteRule="CASCADE")]
		public IRTransaction IRTransaction
		{
			get
			{
				return this._IRTransaction.Entity;
			}
			set
			{
				IRTransaction previousValue = this._IRTransaction.Entity;
				if (((previousValue != value) 
							|| (this._IRTransaction.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._IRTransaction.Entity = null;
						previousValue.EvidencePhotos.Remove(this);
					}
					this._IRTransaction.Entity = value;
					if ((value != null))
					{
						value.EvidencePhotos.Add(this);
						this._IrId = value.Id;
					}
					else
					{
						this._IrId = default(Nullable<int>);
					}
					this.SendPropertyChanged("IRTransaction");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SiteStatus")]
	public partial class SiteStatus : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private System.Nullable<bool> _Status;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnStatusChanging(System.Nullable<bool> value);
    partial void OnStatusChanged();
    #endregion
		
		public SiteStatus()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="Bit")]
		public System.Nullable<bool> Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.DepartmentsInvolved")]
	public partial class DepartmentsInvolved : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private System.Nullable<int> _IRId;
		
		private System.Nullable<int> _DepartmentId;
		
		private EntityRef<IRTransaction> _IRTransaction;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnIRIdChanging(System.Nullable<int> value);
    partial void OnIRIdChanged();
    partial void OnDepartmentIdChanging(System.Nullable<int> value);
    partial void OnDepartmentIdChanged();
    #endregion
		
		public DepartmentsInvolved()
		{
			this._IRTransaction = default(EntityRef<IRTransaction>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IRId", DbType="Int")]
		public System.Nullable<int> IRId
		{
			get
			{
				return this._IRId;
			}
			set
			{
				if ((this._IRId != value))
				{
					if (this._IRTransaction.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIRIdChanging(value);
					this.SendPropertyChanging();
					this._IRId = value;
					this.SendPropertyChanged("IRId");
					this.OnIRIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentId", DbType="Int")]
		public System.Nullable<int> DepartmentId
		{
			get
			{
				return this._DepartmentId;
			}
			set
			{
				if ((this._DepartmentId != value))
				{
					this.OnDepartmentIdChanging(value);
					this.SendPropertyChanging();
					this._DepartmentId = value;
					this.SendPropertyChanged("DepartmentId");
					this.OnDepartmentIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="IRTransaction_DepartmentsInvolved", Storage="_IRTransaction", ThisKey="IRId", OtherKey="Id", IsForeignKey=true, DeleteRule="CASCADE")]
		public IRTransaction IRTransaction
		{
			get
			{
				return this._IRTransaction.Entity;
			}
			set
			{
				IRTransaction previousValue = this._IRTransaction.Entity;
				if (((previousValue != value) 
							|| (this._IRTransaction.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._IRTransaction.Entity = null;
						previousValue.DepartmentsInvolveds.Remove(this);
					}
					this._IRTransaction.Entity = value;
					if ((value != null))
					{
						value.DepartmentsInvolveds.Add(this);
						this._IRId = value.Id;
					}
					else
					{
						this._IRId = default(Nullable<int>);
					}
					this.SendPropertyChanged("IRTransaction");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.IRTransaction")]
	public partial class IRTransaction : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _TicketNo;
		
		private System.Nullable<int> _CrisisId;
		
		private string _From;
		
		private string _Subject;
		
		private string _Room;
		
		private System.Nullable<System.DateTime> _Date;
		
		private string _Status;
		
		private System.Nullable<System.DateTime> _WhenIncidentHappen;
		
		private string _WhenAware;
		
		private string _WhoInvolved;
		
		private string _WhatHappened;
		
		private string _Investigation;
		
		private string _ActionTaken;
		
		private string _Recommendation;
		
		private System.Nullable<System.Guid> _PreparedBy;
		
		private System.Nullable<System.DateTime> _DateSolved;
		
		private EntitySet<EvidencePhoto> _EvidencePhotos;
		
		private EntitySet<DepartmentsInvolved> _DepartmentsInvolveds;
		
		private EntityRef<CrisisCode> _CrisisCode;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnTicketNoChanging(string value);
    partial void OnTicketNoChanged();
    partial void OnCrisisIdChanging(System.Nullable<int> value);
    partial void OnCrisisIdChanged();
    partial void OnFromChanging(string value);
    partial void OnFromChanged();
    partial void OnSubjectChanging(string value);
    partial void OnSubjectChanged();
    partial void OnRoomChanging(string value);
    partial void OnRoomChanged();
    partial void OnDateChanging(System.Nullable<System.DateTime> value);
    partial void OnDateChanged();
    partial void OnStatusChanging(string value);
    partial void OnStatusChanged();
    partial void OnWhenIncidentHappenChanging(System.Nullable<System.DateTime> value);
    partial void OnWhenIncidentHappenChanged();
    partial void OnWhenAwareChanging(string value);
    partial void OnWhenAwareChanged();
    partial void OnWhoInvolvedChanging(string value);
    partial void OnWhoInvolvedChanged();
    partial void OnWhatHappenedChanging(string value);
    partial void OnWhatHappenedChanged();
    partial void OnInvestigationChanging(string value);
    partial void OnInvestigationChanged();
    partial void OnActionTakenChanging(string value);
    partial void OnActionTakenChanged();
    partial void OnRecommendationChanging(string value);
    partial void OnRecommendationChanged();
    partial void OnPreparedByChanging(System.Nullable<System.Guid> value);
    partial void OnPreparedByChanged();
    partial void OnDateSolvedChanging(System.Nullable<System.DateTime> value);
    partial void OnDateSolvedChanged();
    #endregion
		
		public IRTransaction()
		{
			this._EvidencePhotos = new EntitySet<EvidencePhoto>(new Action<EvidencePhoto>(this.attach_EvidencePhotos), new Action<EvidencePhoto>(this.detach_EvidencePhotos));
			this._DepartmentsInvolveds = new EntitySet<DepartmentsInvolved>(new Action<DepartmentsInvolved>(this.attach_DepartmentsInvolveds), new Action<DepartmentsInvolved>(this.detach_DepartmentsInvolveds));
			this._CrisisCode = default(EntityRef<CrisisCode>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TicketNo", DbType="VarChar(50)")]
		public string TicketNo
		{
			get
			{
				return this._TicketNo;
			}
			set
			{
				if ((this._TicketNo != value))
				{
					this.OnTicketNoChanging(value);
					this.SendPropertyChanging();
					this._TicketNo = value;
					this.SendPropertyChanged("TicketNo");
					this.OnTicketNoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CrisisId", DbType="Int")]
		public System.Nullable<int> CrisisId
		{
			get
			{
				return this._CrisisId;
			}
			set
			{
				if ((this._CrisisId != value))
				{
					if (this._CrisisCode.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCrisisIdChanging(value);
					this.SendPropertyChanging();
					this._CrisisId = value;
					this.SendPropertyChanged("CrisisId");
					this.OnCrisisIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[From]", Storage="_From", DbType="VarChar(MAX)")]
		public string From
		{
			get
			{
				return this._From;
			}
			set
			{
				if ((this._From != value))
				{
					this.OnFromChanging(value);
					this.SendPropertyChanging();
					this._From = value;
					this.SendPropertyChanged("From");
					this.OnFromChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Subject", DbType="VarChar(MAX)")]
		public string Subject
		{
			get
			{
				return this._Subject;
			}
			set
			{
				if ((this._Subject != value))
				{
					this.OnSubjectChanging(value);
					this.SendPropertyChanging();
					this._Subject = value;
					this.SendPropertyChanged("Subject");
					this.OnSubjectChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Room", DbType="VarChar(MAX)")]
		public string Room
		{
			get
			{
				return this._Room;
			}
			set
			{
				if ((this._Room != value))
				{
					this.OnRoomChanging(value);
					this.SendPropertyChanging();
					this._Room = value;
					this.SendPropertyChanged("Room");
					this.OnRoomChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="Date")]
		public System.Nullable<System.DateTime> Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="VarChar(50)")]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WhenIncidentHappen", DbType="DateTime")]
		public System.Nullable<System.DateTime> WhenIncidentHappen
		{
			get
			{
				return this._WhenIncidentHappen;
			}
			set
			{
				if ((this._WhenIncidentHappen != value))
				{
					this.OnWhenIncidentHappenChanging(value);
					this.SendPropertyChanging();
					this._WhenIncidentHappen = value;
					this.SendPropertyChanged("WhenIncidentHappen");
					this.OnWhenIncidentHappenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WhenAware", DbType="VarChar(50)")]
		public string WhenAware
		{
			get
			{
				return this._WhenAware;
			}
			set
			{
				if ((this._WhenAware != value))
				{
					this.OnWhenAwareChanging(value);
					this.SendPropertyChanging();
					this._WhenAware = value;
					this.SendPropertyChanged("WhenAware");
					this.OnWhenAwareChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WhoInvolved", DbType="VarChar(MAX)")]
		public string WhoInvolved
		{
			get
			{
				return this._WhoInvolved;
			}
			set
			{
				if ((this._WhoInvolved != value))
				{
					this.OnWhoInvolvedChanging(value);
					this.SendPropertyChanging();
					this._WhoInvolved = value;
					this.SendPropertyChanged("WhoInvolved");
					this.OnWhoInvolvedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WhatHappened", DbType="VarChar(MAX)")]
		public string WhatHappened
		{
			get
			{
				return this._WhatHappened;
			}
			set
			{
				if ((this._WhatHappened != value))
				{
					this.OnWhatHappenedChanging(value);
					this.SendPropertyChanging();
					this._WhatHappened = value;
					this.SendPropertyChanged("WhatHappened");
					this.OnWhatHappenedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Investigation", DbType="VarChar(MAX)")]
		public string Investigation
		{
			get
			{
				return this._Investigation;
			}
			set
			{
				if ((this._Investigation != value))
				{
					this.OnInvestigationChanging(value);
					this.SendPropertyChanging();
					this._Investigation = value;
					this.SendPropertyChanged("Investigation");
					this.OnInvestigationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActionTaken", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string ActionTaken
		{
			get
			{
				return this._ActionTaken;
			}
			set
			{
				if ((this._ActionTaken != value))
				{
					this.OnActionTakenChanging(value);
					this.SendPropertyChanging();
					this._ActionTaken = value;
					this.SendPropertyChanged("ActionTaken");
					this.OnActionTakenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Recommendation", DbType="VarChar(MAX)")]
		public string Recommendation
		{
			get
			{
				return this._Recommendation;
			}
			set
			{
				if ((this._Recommendation != value))
				{
					this.OnRecommendationChanging(value);
					this.SendPropertyChanging();
					this._Recommendation = value;
					this.SendPropertyChanged("Recommendation");
					this.OnRecommendationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PreparedBy", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> PreparedBy
		{
			get
			{
				return this._PreparedBy;
			}
			set
			{
				if ((this._PreparedBy != value))
				{
					this.OnPreparedByChanging(value);
					this.SendPropertyChanging();
					this._PreparedBy = value;
					this.SendPropertyChanged("PreparedBy");
					this.OnPreparedByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateSolved", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateSolved
		{
			get
			{
				return this._DateSolved;
			}
			set
			{
				if ((this._DateSolved != value))
				{
					this.OnDateSolvedChanging(value);
					this.SendPropertyChanging();
					this._DateSolved = value;
					this.SendPropertyChanged("DateSolved");
					this.OnDateSolvedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="IRTransaction_EvidencePhoto", Storage="_EvidencePhotos", ThisKey="Id", OtherKey="IrId")]
		public EntitySet<EvidencePhoto> EvidencePhotos
		{
			get
			{
				return this._EvidencePhotos;
			}
			set
			{
				this._EvidencePhotos.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="IRTransaction_DepartmentsInvolved", Storage="_DepartmentsInvolveds", ThisKey="Id", OtherKey="IRId")]
		public EntitySet<DepartmentsInvolved> DepartmentsInvolveds
		{
			get
			{
				return this._DepartmentsInvolveds;
			}
			set
			{
				this._DepartmentsInvolveds.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CrisisCode_IRTransaction", Storage="_CrisisCode", ThisKey="CrisisId", OtherKey="Id", IsForeignKey=true, DeleteRule="CASCADE")]
		public CrisisCode CrisisCode
		{
			get
			{
				return this._CrisisCode.Entity;
			}
			set
			{
				CrisisCode previousValue = this._CrisisCode.Entity;
				if (((previousValue != value) 
							|| (this._CrisisCode.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CrisisCode.Entity = null;
						previousValue.IRTransactions.Remove(this);
					}
					this._CrisisCode.Entity = value;
					if ((value != null))
					{
						value.IRTransactions.Add(this);
						this._CrisisId = value.Id;
					}
					else
					{
						this._CrisisId = default(Nullable<int>);
					}
					this.SendPropertyChanged("CrisisCode");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_EvidencePhotos(EvidencePhoto entity)
		{
			this.SendPropertyChanging();
			entity.IRTransaction = this;
		}
		
		private void detach_EvidencePhotos(EvidencePhoto entity)
		{
			this.SendPropertyChanging();
			entity.IRTransaction = null;
		}
		
		private void attach_DepartmentsInvolveds(DepartmentsInvolved entity)
		{
			this.SendPropertyChanging();
			entity.IRTransaction = this;
		}
		
		private void detach_DepartmentsInvolveds(DepartmentsInvolved entity)
		{
			this.SendPropertyChanging();
			entity.IRTransaction = null;
		}
	}
}
#pragma warning restore 1591
