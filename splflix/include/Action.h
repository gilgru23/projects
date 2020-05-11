#ifndef ACTION_H_
#define ACTION_H_

#include <string>
#include <iostream>

class Session;

enum ActionStatus{
	PENDING, COMPLETED, ERROR
};


class BaseAction{
public:
	BaseAction();

    BaseAction(const std::string &errorMsg);

    ActionStatus getStatus() const;
	virtual void act(Session& sess)=0;
	virtual std::string toString() const=0;
    virtual BaseAction* Copy();
    virtual ~BaseAction();
    void SetErrorMsg(Session& , std::string&);
protected:
	void complete();
	void error(const std::string& errorMsg);
	std::string getErrorMsg() const;
	std::string OutputStr() const;

private:
    virtual BaseAction* GetCopy()=0;
	std::string errorMsg;
	ActionStatus status;
};

class CreateUser  : public BaseAction {
public:
    virtual void act(Session &sess);
    CreateUser();
    CreateUser(std::string &, std::string &);
    virtual std::string toString() const;
    ~CreateUser();

private:
    virtual BaseAction* GetCopy();
    std::string name;
    std::string algo_code;
};

class ChangeActiveUser : public BaseAction {
public:
    ChangeActiveUser();
    ~ChangeActiveUser();
    ChangeActiveUser(std::string&);
    virtual void act(Session& sess);
	virtual std::string toString() const;
private:
    virtual BaseAction* GetCopy();
    std::string name;
};

class DeleteUser : public BaseAction {
public:
    DeleteUser();
    ~DeleteUser();
    DeleteUser(std::string& name);
    virtual void act(Session & sess);
	virtual std::string toString() const;
private:
    virtual BaseAction* GetCopy();
    std::string name;
};

class DuplicateUser : public BaseAction {
public:
    DuplicateUser(std::string&,std::string&);
	~DuplicateUser();
    DuplicateUser();
    virtual void act(Session & sess);
	virtual std::string toString() const;
private:
    virtual BaseAction* GetCopy();
    std::string old_name;
    std::string new_name;
};

class PrintContentList : public BaseAction {
public:
    ~PrintContentList();
	virtual void act (Session& sess);
	virtual std::string toString() const;
    virtual BaseAction* GetCopy();
};

class PrintWatchHistory : public BaseAction {
public:
	virtual void act (Session& sess);
	virtual std::string toString() const;
    virtual BaseAction* GetCopy();

    virtual ~PrintWatchHistory();
};


class Watch : public BaseAction {
public:
    Watch(long contentId);
	virtual void act(Session& sess);
	virtual std::string toString() const;
    Watch();
    virtual ~Watch();

private:
    virtual BaseAction* GetCopy();
    long contentId;
};


class PrintActionsLog : public BaseAction {
public:
	virtual void act(Session& sess);
	virtual std::string toString() const;
    virtual BaseAction* GetCopy();

    virtual ~PrintActionsLog();

};

class Exit : public BaseAction {
public:
	virtual void act(Session& sess);
	virtual std::string toString() const;
    virtual BaseAction* GetCopy();

    virtual ~Exit();
};
#endif
