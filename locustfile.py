from locust import HttpLocust, TaskSet, task

class MyTaskSet(TaskSet):
    @task
    def index(self):
        self.client.get("/")

class MyLocust(HttpLocust):
    task_set = MyTaskSet
    min_wait = 1000
    max_wait = 5000
