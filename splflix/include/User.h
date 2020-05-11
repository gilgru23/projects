#ifndef USER_H_
#define USER_H_

#include <vector>
#include <string>
#include <unordered_set>
#include <unordered_map>
#include <map>
class Watchable;
class Session;

class User{
public:
    User(const std::string& name);
    virtual ~User();
    virtual User* GetCopy(const std::string& name)=0;
    virtual Watchable* getRecommendation(Session& s) = 0;
    std::string getName() const;
    std::vector<Watchable*> get_history() const;
    virtual void watchContent(Watchable*);


protected:
    std::vector<Watchable*>* UnWatched(Session&);
    std::vector<Watchable*> history;
    virtual User* Copy(const User& other);
private:
    const std::string name;



};


class LengthRecommenderUser : public User {
public:
    ~LengthRecommenderUser();
    LengthRecommenderUser(const std::string& name);
    //LengthRecommenderUser(const LengthRecommenderUser&);
    LengthRecommenderUser(const LengthRecommenderUser&);
    LengthRecommenderUser* GetCopy(const std::string& name);
    virtual Watchable* getRecommendation(Session& s);
    LengthRecommenderUser(LengthRecommenderUser*);
    int GetHistoAvg();




private:
};

class RerunRecommenderUser : public User {
public:
    RerunRecommenderUser(const std::string& name);
    RerunRecommenderUser(const RerunRecommenderUser&);

    RerunRecommenderUser* GetCopy(const std::string& name);
    virtual Watchable* getRecommendation(Session& s);
    virtual ~RerunRecommenderUser();
private:
     int RerunIndex;
};
class GenreRecommenderUser : public User {
public:
    GenreRecommenderUser(const std::string& name);
    GenreRecommenderUser(const GenreRecommenderUser&);

    GenreRecommenderUser* GetCopy(const std::string& name);
    void watchContent(Watchable*);
    virtual Watchable* getRecommendation(Session& s);

    virtual ~GenreRecommenderUser();
    GenreRecommenderUser &operator=(const GenreRecommenderUser & other);
protected:
    void Copy(const GenreRecommenderUser & other);
private:
    void Clean();
    std::map<std::string,int> *tagsCount;


};

#endif
