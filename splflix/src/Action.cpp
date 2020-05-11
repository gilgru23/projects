#include "../include/Action.h"
#include "../include/User.h"
#include "../include/Session.h"
#include "../include/Watchable.h"
#include <vector>
#include <string>

using string=std::string;
using vectorSt= std::vector<string>;

//base action
BaseAction::BaseAction():errorMsg(), status(ActionStatus::PENDING) {}
BaseAction::BaseAction(const std::string &errorMsg):
        errorMsg(errorMsg), status(ActionStatus::PENDING){}
void BaseAction::complete()
{
    status=ActionStatus::COMPLETED;
}

void BaseAction::error(const std::string &_errorMsg)
{
    status=ActionStatus::ERROR;
    errorMsg=_errorMsg;
    std::cout<<"ERROR: "+errorMsg<<std::endl;
}

std::string BaseAction::OutputStr() const {
    string output;
    if (status==ActionStatus::COMPLETED)
    {
        output = " COMPLETED";
    }
    else
    {
        output= " ERROR:"+errorMsg;
    }
    return output;
}

BaseAction *BaseAction::Copy() {
    BaseAction* ba = this->GetCopy();
    ba->status=status;
    ba->errorMsg=errorMsg;
    return ba;
}

BaseAction::~BaseAction() {

}

void BaseAction::SetErrorMsg(Session& sess , std::string & msg) {
    sess.AddActionToLog(this);
    error(msg);
}

//
//CreateUser
void CreateUser::act(Session &sess) {
    sess.AddActionToLog(this);
    std::string err= sess.AddUser(name,algo_code);
    if (err.length()==0)
    {
        complete();
    }
    else
    {
        error(err);
    }
}

CreateUser::CreateUser(std::string & name, std::string &algo_code):
        name(name),algo_code(algo_code){}

CreateUser::CreateUser():BaseAction(),name(),algo_code() {}

std::string CreateUser::toString() const {
    string output = OutputStr();
    return "CreateUser"+output;
}

BaseAction *CreateUser::GetCopy() {
    return new CreateUser(name,algo_code);
}

CreateUser::~CreateUser() {

}

//
//ChangeActiveUser
void ChangeActiveUser::act(Session &sess) {
    sess.AddActionToLog(this);
    std::string err=sess.SwitchUser(name);
    if (err.length()==0)
    {
        complete();
    }
    else
    {
        error(err);
    }
}

ChangeActiveUser::ChangeActiveUser(std::string& name):
        name(name) {}

std::string ChangeActiveUser::toString() const {
    string output = OutputStr();
    return "ChangeActiveUser"+output;
}

BaseAction *ChangeActiveUser::GetCopy() {
    return new ChangeActiveUser(name);
}

ChangeActiveUser::~ChangeActiveUser() {

}

ChangeActiveUser::ChangeActiveUser(): BaseAction(), name() {}

//
//DeleteUser
DeleteUser::DeleteUser(std::string& name):
        name(name) {}

void DeleteUser::act(Session &sess) {
    sess.AddActionToLog(this);
    std::string err= sess.RemoveUser(name);
    if (err=="")
        complete();
    else
        error(err);
}

std::string DeleteUser::toString() const {
    string output = OutputStr();
    return "DeleteUser"+output;
}

BaseAction *DeleteUser::GetCopy() {
    return new DeleteUser(name);
}

DeleteUser::~DeleteUser() {

}

DeleteUser::DeleteUser() : BaseAction(), name() {}

//
//DuplicateUser
DuplicateUser::DuplicateUser(std::string & oldName, std::string & newName):
        old_name(oldName),new_name(newName){}

void DuplicateUser::act(Session &sess) {
    sess.AddActionToLog(this);
    std::string err= sess.CopyUser(old_name, new_name);
    if (err=="")
        complete();
    else
        error(err);
}

std::string DuplicateUser::toString() const {
    string output = OutputStr();
    return "DuplicateUser"+output;
}

BaseAction *DuplicateUser::GetCopy() {
    return new DuplicateUser(old_name,new_name);
}

DuplicateUser::~DuplicateUser() {

}

DuplicateUser::DuplicateUser():BaseAction(),old_name(),new_name() {}

//
//Watch
Watch::Watch(long contentId) : contentId(contentId) {}

void Watch::act(Session &sess)
{
    sess.AddActionToLog(this);

    if(contentId<(long)sess.getContent().size())
    {
        Watchable *w = sess.getContent().at(contentId);
        User*  u = sess.GetActiveUser();
        u->watchContent(w);
        std::cout<<"watching "+w->toString()<<std::endl;

        Watchable *nextW = w->getNextWatchable(sess);

        if (nextW != nullptr)
        {
            string recLine="We recommend watching "+nextW->toString()+", continue watching? [y/n]";
            bool ans=false;
            string input;
            while(ans==false){

                std::cout<<recLine<<std::endl;
                std::getline(std::cin,input);

                if (input=="y")
                {
                    ans=true;
                    Watch* watchNext = new Watch((*nextW).getId());
                    watchNext->act(sess);
                }
                else if(input=="n")
                {
                    ans=true;
                }
            }
        }
        complete();
    }
    else
        {
            error("unable to play media "+std::to_string(contentId+1));
        }
}

std::string Watch::toString() const {
    string output = OutputStr();
    return "Watch"+output;
}

BaseAction *Watch::GetCopy() {
    return new Watch(contentId);
}

Watch::~Watch() {

}

Watch::Watch():BaseAction(), contentId() {}


//PrintWatchHistory
std::string PrintWatchHistory::toString() const {
    string output = OutputStr();
    return "PrintWatchHistory"+output;
}

void PrintWatchHistory::act(Session &sess)
{
    sess.AddActionToLog(this);
    std::cout<<"Watch history for "+sess.GetActiveUser()->getName()<<std::endl;
    std::vector<Watchable*> content = sess.GetActiveUser()->get_history();
    std::vector<Watchable*>::iterator ptr = content.begin();
    int i=1;
    for (;ptr<content.end();ptr++)
    {
        std::cout<<std::to_string(i)+". "+(*ptr)->toString()<<std::endl;
        i++;
    }
    complete();

}

BaseAction *PrintWatchHistory::GetCopy() {
    return new PrintWatchHistory();
}

PrintWatchHistory::~PrintWatchHistory() {

}

//


std::string PrintActionsLog::toString() const {
    string output = OutputStr();
    return "PrintActionsLog"+output;
}

void PrintActionsLog::act(Session &sess) {
    std::vector<BaseAction*> log = sess.GetActionsLog();
    std::reverse_iterator<std::vector<BaseAction *>::iterator> ptr = log.rbegin();
    for(; ptr != log.rend();++ptr )
    {
        std::cout<< (*ptr)->toString()<<std::endl;
    }
    sess.AddActionToLog(this);
    complete();
}

BaseAction *PrintActionsLog::GetCopy() {
    return new PrintActionsLog();
}

PrintActionsLog::~PrintActionsLog() {

}


//PrintContentList
std::string PrintContentList::toString() const {
    string output = OutputStr();
    return "PrintContentList"+output;
}
void PrintContentList::act(Session &sess) {
    sess.AddActionToLog(this);
    std::vector<Watchable*> content = sess.getContent();
    std::vector<Watchable*>::iterator ptr = content.begin();
    for (;ptr<content.end();ptr++)
    {
        string tagsStr;
        vectorSt tags = (*ptr)->getTags();
        vectorSt::iterator itr= tags.begin();

        for (;itr<tags.end()-1;itr++)
        {
            tagsStr+=(*itr)+", ";
        }
        tagsStr+=tags.back();

        std::cout<<std::to_string((*ptr)->getId()+1)+". "+(*ptr)->toString()+" "+std::to_string((*ptr)->getLength())
        +" minutes ["+tagsStr+"]"<<std::endl;
    }
    complete();
}

BaseAction *PrintContentList::GetCopy() {
    return new PrintContentList();
}

PrintContentList::~PrintContentList() {

}

//Exit
void Exit::act(Session &sess) {
    sess.AddActionToLog(this);
    sess.disconnect();
    complete();
}

std::string Exit::toString() const {
    string output = OutputStr();
    return "Exit"+output;
}

BaseAction *Exit::GetCopy() {
    return new Exit();
}

Exit::~Exit() {

}
