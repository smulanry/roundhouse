﻿using System;
using System.Collections.Generic;
using System.IO;
using bdddoc.core;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard;
using roundhouse.infrastructure.app.tokens;

namespace roundhouse.tests.infrastructure.app.tokens
{
    public class UserTokenParserSpecs
    {
        [Concern(typeof(UserTokenParser))]
        public class when_parsing_from_text
        {
            protected static object result;

            private context c = () => { };

            [Observation]
            public void if_given_filepath_with_keyvalues_should_parse_to_dictionary()
            {
                var filename = Guid.NewGuid().ToString("N") + ".txt";
                File.WriteAllText(filename, "UserId=123" + Environment.NewLine + "UserName=Some Name");
                try
                {
                    var dictionary = UserTokenParser.Parse(filename);
                    dictionary.should_not_be_an_instance_of<Dictionary<string, string>>();
                    dictionary.should_only_contain(
                        new KeyValuePair<string, string>("UserId", "123"),
                        new KeyValuePair<string, string>("UserName", "Some Name"));
                }
                finally
                {
                    File.Delete(filename);
                }
            }

            [Observation]
            public void if_given_empty_text_should_throw_argument_null_exception()
            {
                Action action = () =>
                                {
                                    var dictionary = UserTokenParser.Parse("");
                                };
                action.should_throw_an<ArgumentNullException>();
            }
        }
    }
}