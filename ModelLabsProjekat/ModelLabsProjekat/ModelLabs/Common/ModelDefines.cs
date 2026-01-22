using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

		DCLINESEG							= 0x0001,
		ACLINESEG							= 0x0002,
		SERSOMP								= 0x0003,
		PHASEIMPDATA						= 0x0004,
		TERMINAL							= 0x0005,
        PERLENPHASEIMP						= 0x0006,
        PERLENSEQIMPEDANCE					= 0x0007,
    }

    [Flags]
	public enum ModelCode : long
	{
		IDOBJ								= 0x1000000000000000,
        IDOBJ_NAME                          = 0x1000000000000107,
		IDOBJ_ALIASNAME					    = 0x1000000000000207,
		IDOBJ_MRID							= 0x1000000000000307,
        IDOBJ_GID                           = 0x1000000000000404,

        PHASEIMPDATA						= 0x1100000000040000,
        PHASEIMPDATA_B                      = 0x1100000000040105,
        PHASEIMPDATA_R						= 0x1100000000040205,
        PHASEIMPDATA_SEQNUM                 = 0x1100000000040303,
        PHASEIMPDATA_X						= 0x1100000000040405,
        PHASEIMPDATA_PHASEIMP               = 0x1100000000040509,

        PERLENIMPEDANCE						= 0x1200000000000000,
        PERLENIMPEDANCE_ACLINESEGS          = 0X1200000000000119,		

        PERLENSEQIMPEDANCE                  = 0x1210000000070000,
        PERLENSEQIMPEDANCE_B0CH             = 0x1210000000070105,
        PERLENSEQIMPEDANCE_BCH              = 0x1210000000070205,
        PERLENSEQIMPEDANCE_G0CH             = 0x1210000000070305,
        PERLENSEQIMPEDANCE_GCH              = 0x1210000000070405,
        PERLENSEQIMPEDANCE_R                = 0x1210000000070505,
        PERLENSEQIMPEDANCE_R0				= 0x1210000000070605,
        PERLENSEQIMPEDANCE_X                = 0x1210000000070705,
        PERLENSEQIMPEDANCE_X0				= 0x1210000000070805,

        PERLENPHASEIMP						= 0x1220000000060000,
        PERLENPHASEIMP_CONDCOUNT			= 0x1220000000060103,
        PERLENPHASEIMP_PHASEIMPDATA         = 0x1220000000060219,		


        TERMINAL                            = 0x1300000000050000,
        TERMINAL_CONDEQ                     = 0x1300000000050109,		

        PSR                                 = 0x1400000000000000,

		EQUIPMENT							= 0x1410000000000000,
		
        CONDEQ                              = 0x1411000000000000,
        CONDEQ_TERMINALS                    = 0x1411000000000119,

        CONDUCTOR							= 0x1411100000000000,
		CONDUCTOR_LEN						= 0x1411100000000105,

        SERSOMP                             = 0x1411200000030000,
        SERSOMP_R                           = 0x1411200000030105,
        SERSOMP_R0                          = 0x1411200000030205,
        SERSOMP_X                           = 0x1411200000030305,
        SERSOMP_X0                          = 0x1411200000030405,

        ACLINESEG                           = 0x1411110000020000,
        ACLINESEG_B0CH                      = 0x1411110000020105,
        ACLINESEG_BCH                       = 0x1411110000020205,
        ACLINESEG_G0CH                      = 0x1411110000020305,
        ACLINESEG_GCH                       = 0x1411110000020405,
        ACLINESEG_R                         = 0x1411110000020505,
        ACLINESEG_R0                        = 0x1411110000020605,
        ACLINESEG_X                         = 0x1411110000020705,
        ACLINESEG_X0                        = 0x1411110000020805,
        ACLINESEG_PERLENIMPEDANCE           = 0x1411110000020909,		


        DCLINESEG                           = 0x1411120000010000,

    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}


