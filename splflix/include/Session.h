#ifndef SESSION_H_
#define SESSION_H_

#include <vector>
#include <unordered_map>
#include <string>
#include "Action.h"
#include "User.h"
#include "Watchable.h"

class Session{
public:
    Session(const std::string &configFilePath);
    Session(const Session&);
    Session& operator=(const Session&);
    Session(Session&&);
    Session& operator=(Session&&);
    ~Session();
    void start();
    User* GetActiveUser() const;
    void AddActionToLog(BaseAction*);
    std::string AddUser(std::string&, std::string&);
    User* FindUser(std::string&);
    std::string SwitchUser(std::string&);
    std::string RemoveUser(std::string &name);
    std::string CopyUser(std::string &original_name, std::string& new_name);
    const std::vector<Watchable*>& getContent() ;
    void parseInput(std::vector<std::string>&,std::string&);
    void disconnect();
    const std::vector<BaseAction*>& GetActionsLog();
    void CallAction(std::vector<std::string> &vec);



private:
    std::vector<Watchable*> content;
    std::vector<BaseAction*> actionsLog;
    std::unordered_map<std::string,User*> userMap;
    User* activeUser;
    bool stop;
    void init();
    void readData(const std::string&);
    void Copy(const Session&);
    void Steal(Session&);
    void Clean();


};
#endif
