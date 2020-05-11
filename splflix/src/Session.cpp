
#include "../include/Session.h"
#include <iostream>
#include <fstream>
#include <sstream>
#include "../include/Watchable.h"
#include "../include/json.hpp"
#include "../include/User.h"
#include "../include/Action.h"
using namespace std;
using string=std::string;
using json = nlohmann::json;


Session::Session(const std::string &configFilePath):
        content(std::vector<Watchable*>()),actionsLog(std::vector<BaseAction*>()),
        userMap(std::unordered_map<std::string,User*>()),activeUser(nullptr),stop(false)
{
    readData(configFilePath);
}

User* Session::GetActiveUser() const  {
    return activeUser;
}

void Session::readData(const string& path) {
    std::ifstream ifs (path);
    json j;
    ifs >> j;

    int idx=0;
    while(j["movies"][idx]!=nullptr)
    {
        int tag=0;
        vector<std::string> tags;
        while(j["movies"][idx]["tags"][tag]!= nullptr)
        {
            tags.push_back(j["movies"][idx]["tags"][tag]);
            tag++;
        }
        content.push_back(new Movie(idx,j["movies"][idx]["name"],j["movies"][idx]["length"],tags));
        idx++;
    }
    int inx =0;
    while(j["tv_series"][inx]!=nullptr)
    {
        int tag=0;
        vector<std::string> tags;
        while(j["tv_series"][inx]["tags"][tag]!= nullptr)
        {
            tags.push_back(j["tv_series"][inx]["tags"][tag]);
            tag++;
        }
        string series_name = j["tv_series"][inx]["name"];
        int length = j["tv_series"][inx]["episode_length"];

        Episode* last;
        int season=0;
        while(j["tv_series"][inx]["seasons"][season]!=nullptr)
        {
            int s_size = j["tv_series"][inx]["seasons"][season];
            for(int e=1;e<=s_size;e++)
            {
                last = new Episode(idx,series_name,length,season+1,e,tags);
                content.push_back(last);
               idx++;
            }
            season++;
        }
        inx++;
        if(last != nullptr)
            last->SetNextEpId(-1);
    }
}

void Session::AddActionToLog(BaseAction * action) {
    actionsLog.push_back(action);
}

std::string Session::AddUser( std::string & name, std::string & algo) {
    std::string err;
    if(FindUser(name)== nullptr)
    {
        User * u=nullptr;
        if(algo=="len"){
            u=new LengthRecommenderUser(name);
        }
        else if(algo=="rer"){
            u=new RerunRecommenderUser(name);
        }
        else if(algo=="gen"){
            u= new GenreRecommenderUser(name);
        }

        if(u!=nullptr)
        {
            userMap.insert({name,u});
        }
        else
        {
            err="illegal recommendation algorithem code";
        }

    }
    else
        err="name is already used";
    return err;
}

User *Session::FindUser(std::string & name) {
    User* output= nullptr;
    try
    {
        output = userMap.at(name);
    }
    catch (const out_of_range &e)
    {}
    return output;
}

std::string Session::SwitchUser(std::string & name) {
    std::string err;
    User* temp= nullptr;
    try
    {
        temp = userMap.at(name);
    }
    catch (const out_of_range &e)
    {}
    if (temp!= nullptr)
    {
        activeUser=temp;
    }
    else
        err="no such user ["+name+"]";
    return err;
}

std::string Session::RemoveUser(std::string & name) {
    std::string err;
    bool found=false;
    auto ptr = userMap.begin();
    for(;!found && ptr!=userMap.end();ptr++)
    {
        if(ptr->first==name) {
            found=true;
            delete ptr->second;
            userMap.erase(ptr);
            break;
        }
    }
    if(!found)
        err ="deletion failed";
    return err;
}

std::string Session::CopyUser(std::string &original_name, std::string &new_name) {
    std::string err;
    if (FindUser(new_name)==nullptr)
    {
        User* new_user= nullptr;
        try
        {
            new_user = userMap.at(original_name)->GetCopy(new_name);
            userMap.insert({new_name,new_user});
        }
        catch (const out_of_range &e)
        {
            err="no such user ["+original_name+"]";
        }
    }
    else
        err = "the name ["+original_name+"] is already taken";
    return err;
}

const vector<Watchable *> & Session::getContent() {
    return content;
}

void Session::init() {
    std::string name = "default";
    std::string algo = "len";
    if(FindUser(name)==nullptr)
    {
        CreateUser* c = new CreateUser(name,algo);
        c->act(*this);
        ChangeActiveUser* ca =new ChangeActiveUser(name);
        ca->act(*this);
    }
}


void Session::start() {
    init();
    std::cout << "SPLFLIX is now on!"<<std::endl;
    stop = false;
    while (!stop) {
        std::string command="";
        while (command=="")  {std::getline(std::cin,command);}
        std::vector<string> CommandVec;
        parseInput(CommandVec,command);
        CallAction(CommandVec);
    }
}


void Session::parseInput(std::vector<std::string>& vec,std::string& command) {
        istringstream iss(command);
        copy(istream_iterator<string>(iss),
             istream_iterator<string>(),
             back_inserter(vec));
}

void Session::CallAction(vector<std::string> &vec) {
    string first=vec.at(0);
    bool numbers =true;
    string argsMiss = "arguments missing";
    long episode;
    try{ episode = std::stol(vec.at(1));}
    catch(std::invalid_argument) {numbers=false;}
    catch(std::out_of_range){numbers=false;}

    int size=vec.size();

    if(first=="exit" )
    {
        Exit* x=new Exit();
        x->act(*this);
    }

    else if(first=="createuser")
    {
        if(size>=3)
        {
            CreateUser* x=new CreateUser(vec.at(1),vec.at(2));
            x->act(*this);
        }
        else{
            CreateUser* x=new CreateUser();
            x->SetErrorMsg(*this,argsMiss);
        }
    }

    else if(first=="changeuser")
    {
        if(size>=2)
        {
            ChangeActiveUser* x=new ChangeActiveUser(vec.at(1));
            x->act(*this);
        }
        else{
            ChangeActiveUser* x=new ChangeActiveUser();
            x->SetErrorMsg(*this,argsMiss);
        }
    }
    else if(first=="deleteuser")
    {
        if(size>=2)
        {
            DeleteUser *x =new DeleteUser(vec.at(1));
            x->act(*this);
        }
        else{
            DeleteUser* x=new DeleteUser();
            x->SetErrorMsg(*this,argsMiss);
        }
    }
    else if(first=="dupuser")
    {
        if(size>=3)
        {
            DuplicateUser *x =new DuplicateUser(vec.at(1),vec.at(2));
            x->act(*this);
        }
        else{
            DuplicateUser* x=new DuplicateUser();
            x->SetErrorMsg(*this,argsMiss);
        }
    }
    else if(first=="content")
    {
        PrintContentList *x =new PrintContentList();
        x->act(*this);
    }
    else if(first=="watchist")
    {
        PrintWatchHistory *x =new PrintWatchHistory();
        x->act(*this);
    }
    else if(first=="watch" && numbers)
    {
        if(size>=2)
        {
            Watch *x =new Watch(episode-1);
            x->act(*this);
        }
        else{
            Watch* x=new Watch();
            x->SetErrorMsg(*this,argsMiss);
        }

    }
    else if(first=="log")
    {
        PrintActionsLog *x =new PrintActionsLog();
        x->act(*this);
    }
}

void Session::disconnect() {
    stop=true;
}

const std::vector<BaseAction*> &Session::GetActionsLog() {
    return actionsLog;
}




Session::~Session() {
    Clean();
}

Session::Session(const Session & other):
        content(std::vector<Watchable*>()),actionsLog(std::vector<BaseAction*>()),
        userMap(std::unordered_map<std::string,User*>()),activeUser(nullptr),stop(false) {
    Copy(other);
}

Session &Session::operator=(const Session &other) {
    if(&other !=this)
    {
        Clean();
        Copy(other);
    }
    return *this;
}

Session::Session(Session &&other):
    content(std::vector<Watchable*>()),actionsLog(std::vector<BaseAction*>()),
    userMap(std::unordered_map<std::string,User*>()),activeUser(nullptr),stop(false)
    {
    Steal(other);
}

Session &Session::operator=(Session &&other) {
    if(&other !=this)
    {
       Clean();
       Steal(other);
    }
    return *this;
}

void Session::Copy(const Session &other) {
    content.clear();
    auto ptrContent = other.content.begin() ;
    for(;ptrContent<other.content.end();ptrContent++)
    {
        content.push_back((*ptrContent)->GetCopy());
    }

    userMap.clear();
    auto ptrMap = other.userMap.begin();
    for(;ptrMap!=other.userMap.end();++ptrMap)
    {
        std::string name =(*ptrMap).first;
        userMap.insert({name,(*ptrMap).second->GetCopy(name)});
    }

    actionsLog.clear();
    auto ptrAct = other.actionsLog.begin();
    for (; ptrAct < other.actionsLog.end(); ++ptrAct)
    {
        actionsLog.push_back((*ptrAct)->Copy());
    }

    if(other.activeUser!= nullptr)
    {
        std::string temp_name= other.activeUser->getName();
        activeUser = FindUser(temp_name);
    }
    else
        activeUser=nullptr;
}

void Session::Steal(Session &other) {
    content=other.content;
    other.content.clear();
    userMap=other.userMap;
    other.userMap.clear();
    actionsLog = other.actionsLog;
    other.actionsLog.clear();
    activeUser=other.activeUser;
    other.activeUser= nullptr;
}

void Session::Clean() {
    //content.clear();
    auto ptrContent = content.begin() ;
    for(;ptrContent<content.end();ptrContent++)
    {
        delete *ptrContent;
    }
   content.clear();


    auto ptrMap = userMap.begin();
    for(;ptrMap!=userMap.end();++ptrMap)
    {
        delete (*ptrMap).second;
        (*ptrMap).second= nullptr;
    }
    userMap.clear();


    auto ptrAct = actionsLog.begin();
    for (; ptrAct < actionsLog.end(); ++ptrAct)
    {
        delete *ptrAct;
    }
    actionsLog.clear();

    activeUser=nullptr;


}





