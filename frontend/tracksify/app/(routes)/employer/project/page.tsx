"use client";
import Logo from "@/components/logo";
import Link from "next/link";

import React from "react";

const projects = () => {
  return (
    <main>
      <div className="  flex  items-center p-4 bg-white"></div>
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
