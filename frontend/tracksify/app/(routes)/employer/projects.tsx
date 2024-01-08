"use client";
import Logo from "@/components/logo";
import Link from "next/link";

import React from "react";

const projects = () => {
  return (
    <main>
      <div className="  flex  items-center p-4 bg-white">
        <div className="p-4  ">
          <Logo />
        </div>

        <div className="space-x-4 ">
          <a
            href="./app/page.tsx"
            className=" bg-color_hover rounded-full px-2 py-2 m-2 hover:text-text_secondary font-sm hover:text-black text-text_secondary"
          >
            Home
          </a>
          <a href="#" className="text-text_tertiary  font-sm">
            Employee
          </a>
          <a href="#" className="text-text_tertiary font-sm">
            Project
          </a>
          <a
            href="#"
            className=" border rounded-full  bg-text_secondary  text-white  font-sm px-3 py-2 hover:bg-color_hover hover:text-text_tertiary"
          >
            {" "}
            FO
          </a>
        </div>
      </div>
      <div className="bg-color_hover h-screen">
        <h1 className="text-2xl text-text_tertiary pt-8 pl-40 font-bold">
          Good Morning Mathilda,
        </h1>
      </div>
      <div className="bg-white h-half w-3/4 mx-auto">
        <div className="container mx-auto p-4">
          <div className="project-item p-2 border rounded mb-2 flex justify-between items-center">
            <span>Project 1</span>
            <span className="status-label bg-green-500 text-white px-2 py-1 rounded">
              Completed
            </span>
          </div>
          <div className="project-item p-2 border rounded mb-2 flex justify-between items-center">
            <span>Project 2</span>
            <span className="status-label bg-red-500 text-white px-2 py-1 rounded">
              Delay
            </span>
          </div>
          <div className="project-item p-2 border rounded mb-2 flex justify-between items-center">
            <span>Project 3</span>
            <span className="status-label bg-yellow-500 text-white px-2 py-1 rounded">
              In Progress
            </span>
          </div>
          <div className="project-item p-2 border rounded mb-2 flex justify-between items-center">
            <span>Project 4</span>
            <span className="status-label bg-green-500 text-white px-2 py-1 rounded">
              Completed
            </span>
          </div>
          <div className="project-item p-2 border rounded mb-2 flex justify-between items-center">
            <span>Project 5</span>
            <span className="status-label bg-green-500 text-white px-2 py-1 rounded">
              Completed
            </span>
          </div>
        </div>
      </div>
    </main>
  );
};

export default projects;
