"use client";
import Link from "next/link";
import Logo from "@/components/logo";

import React, { useState } from "react";

const page = () => {
  return (
    <div className="">
      <main className="">
        <div className="bg-color_hover h-screen ">
          <div className="p-10 pl-20">
            <h1 className="text-2xl text-text_tertiary pt-8 pl-40 font-bold">
              Good Morning Mathilda,
            </h1>

            <div className="bg-white h-half w-3/4 mx-auto  ">
              <h1 className="text-text_tertiary font-bold  text-lg  pt-10 pl-6 mt-4">
                Recent Projects
              </h1>
              <div className="grid grid-cols-5 gap-2 p-10   ">
                <h3 className=" text-text_tertiary font-bold  text-sm p-5">
                  Project Name
                </h3>
                <h3 className="text-text_tertiary font-bold text-sm  p-5">
                  Start Date
                </h3>
                <h3 className=" text-text_tertiary font-bold text-sm p-5">
                  Due Date
                </h3>

                <h3 className="text-text_tertiary font-bold text-sm p-5">
                  Status
                </h3>

                <h3 className="text-text_tertiary font-bold text-sm p-5"></h3>

                {/* GRID 1 */}
                <p className=" hover:bg-color_hover p-5">Project 1</p>
                <p className=" hover:bg-color_hover p-5">Dec 1, 2023</p>
                <p className=" hover:bg-color_hover p-5">Dec 31, 2023</p>

                <select
                  className="p-5"
                  onChange={(e) => console.log(e.target.value)}
                >
                  <option value="In Progress" className="text-yellow-500">
                    In Progress
                  </option>
                  <option value="Pending" className="text-red-500">
                    Pending
                  </option>
                  <option
                    value="Completed  "
                    className="text-green-500 border-none"
                  >
                    Completed
                  </option>
                </select>
                <div className="flex justify-end pr-20.3 pb-4 ">
                  <Link href="/checkin">
                    <button className="bg-text_secondary text-white hover:text-text_tertiary hover:bg-color_hover rounded px-4 py-2 mt-4">
                      Check In
                    </button>
                  </Link>
                </div>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
};

export default page;
